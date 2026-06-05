using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Novell.Directory.Ldap.Utilclass;
using System.Data;
using System.Net.Http;
using System.Runtime;
using System.Threading.Tasks;
using System;
using DiagnofirmAdmin.Contaxdb;
using DiagnofirmAdmin.Control;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace DiagnofirmAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        protected Dictionary<string, string> _jsonData;
        public readonly LoginControl _actionCtrl;
        public readonly EncryptDecrypt _protect = new EncryptDecrypt();
        private readonly ApplicationSettings _settings;
        private readonly OidcSettings _oidcSettings;
        private readonly ILogger<LoginController> _logger;
        private readonly string authMode;
        public LoginController(
            IOptions<ApplicationSettings> settings,
            IOptions<OidcSettings> options,
            ILogger<LoginController> logger)
        {
            _actionCtrl = new LoginControl();
            _settings = settings.Value;
            _oidcSettings = options.Value;
            _logger = logger;
            authMode = _settings.AuthMode;
        }

        [HttpGet]
        [Route("SSO")]
        public async Task<IActionResult> SSO()
        {

            try
            {
                string appUrl = "http://localhost:4200";//_applicationSettings.APP_URL;
                string serviceUrl = "https://" + Request.Host.Value.ToString() + "" + Request.Path.ToString();
                string sgid = RetrieveSGIdFromTicket(HttpContext.Request.Query["ticket"].ToString(), serviceUrl, "https://uat.websso.saint-gobain.com/cas/");

                if (!string.IsNullOrEmpty(sgid))
                {
                    string token = ValidateUser(sgid);

                    if (token != null)
                    {
                        setTokenCookie("WEB_TOKEN", token);
                        setTokenCookie("SG_ID", sgid);

                        string redirectionUrl = _settings.APP_URL + "authorize";
                        return Redirect(redirectionUrl);
                    }
                    else
                    {
                        appUrl = _settings.APP_URL + "notfound";
                        return Redirect(appUrl);
                    }
                }
                else
                {
                    return Redirect(_settings.APP_URL + "/login?code=temp");
                }

            }
            catch (Exception ex)
            {
                return Redirect(_settings.APP_URL + "/login?msg=" + ex.Message.ToString());
            }

        }

        [HttpPost]
        [Route("ValidateLDAP")]
        public IActionResult ValidateLDAP(LoginModelLDAP login)
        {
            try
            {
                LoginLDAPValidationModel loginval = new LoginLDAPValidationModel();
                loginval.SGID = login.SGID;
                DataSet ds = _actionCtrl.ValidateLDAP(loginval);

                if (ds == null)
                {
                    var responseValue = new { status = "Failed", response = "", message = "Access denied. Contact Admin!" };
                    return Ok(responseValue);
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0].ItemArray[0].ToString() == "success")
                    {
                        string token = ValidateUser(loginval.SGID);

                        if (token != null)
                        {
                            setTokenCookie("WEB_TOKEN", token);
                        }

                        //string PwdValid = _protect.SGDecryption(ds.Tables[0].Rows[0].ItemArray[12].ToString());
                        //PwdValid = EncryptedpasswordToDataRow(ds.Tables[0].Rows[0].ItemArray[9].ToString());

                        string PwdValid = ds.Tables[0].Rows[0].ItemArray[12].ToString();

                        if (login.Password == PwdValid)
                        {
                            var responseValue = new { status = "success", response = ds, jwttoken = token, message = "logged in successfully" };
                            return Ok(responseValue);
                        }
                        else
                        {
                            var responseValue = new { status = "information", response = "Invalid Password", message = "Invalid Password." };
                            return Ok(responseValue);
                        }
                    }
                    else
                    {
                        var responseValue = new { status = "information", response = "Invalid  Username", message = "Invalid  Username" };
                        return Ok(responseValue);
                    }
                }

            }
            catch (Exception ex)
            {
                var responseValue = new { status = "Error", response = ex.Message, message = "Invalid  Username or Password." };
                return Ok(responseValue);
            }

        }

        [HttpPost]
        [Route("ValidateSgid")]
        public IActionResult ValidateSgid(LoginModel loginModel)
        {
            try
            {
                //Local
                string token = "eyJhbGciOiJIUzM4NCIsInR5cCI6IkpXVCJ9.eyJTR0lEIjoiQTAxNjczNjciLCJuYmYiOjE3MjQ4NDAxNDgsImV4cCI6MTcyNDkyNjU0OCwiaWF0IjoxNzI0ODQwMTQ4fQ.gqmUKP6adI0TbwwO4kh_APgjl9FvGAib8NcyFiHsfc3kuze-xaSiXmRo0I0aLOAr";
                string Sgid = "A0167367";

                //Hosting
                //string token = Request.Cookies["WEB_TOKEN"];
                //string Sgid = Request.Cookies["SG_ID"];

                //string Sgidvalue = Request.Cookies["SG_ID"];

                loginModel.sgid = Sgid;

                DataSet ds = _actionCtrl.ValidateSgid(loginModel);
                if (ds == null)
                {
                    var responseValue = new { status = "failed", response = "", message = "Error Occured. DataSet is Null." };
                    return Ok(responseValue);
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var responseValue = new { status = "success", response = ds, jwttoken = token, message = "" };
                    return Ok(responseValue);
                }
                else
                {
                    var responseValue = new { status = "information", response = ds };
                    return Ok(responseValue);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ValidateSgid:" + ex.Message);
                var responseValue = new { status = "Error", response = "", message = "" };
                return Ok(responseValue);
            }
        }

        private string RetrieveSGIdFromTicket(string ticket, string service, string UAT_URL)
        {
            try
            {

                using (HttpClient httpClient = new HttpClient())
                {
                    string validateUrl = UAT_URL + "validate?" + "ticket=" + ticket + "&" + "service=" + service;
                    var response = httpClient.GetAsync(validateUrl).Result;
                    string resp = response.Content.ReadAsStringAsync().Result;
                    string[] arrUID = resp.Split('\n');
                    if (arrUID[0].ToUpper() == "YES")
                    {
                        return arrUID[1];
                    }
                }
                return null;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.StackTrace.ToString());
            }
        }

        private string ValidateUser(string sSgid)
        {
            try
            {
                LoginValidationModel login = new LoginValidationModel();
                login.SGID = sSgid;
                DataSet ds = _actionCtrl.Validateuser(login);
                if (ds == null)
                {
                    _logger.LogInformation("Login Failed. SGID: " + login.SGID);
                    return null;
                }
                else if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0].ItemArray[0].ToString() == "success")
                    {
                        string sgid = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                        JwtTokenHandler jwtTokenHandler = new(_settings, sgid);
                        return jwtTokenHandler.createToken();
                    }
                    else
                    {
                        _logger.LogInformation("Unauthorized User");
                        return null;
                    }
                }
                else
                {
                    _logger.LogInformation("Login failed");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Login Exception:" + ex.Message + " Stack Trace:" + ex.StackTrace);
                return null;
            }

        }

        private void setTokenCookie(string tokenKey, string token)
        {
            CookieOptions options = null;
            if (!Request.Host.Value.Contains("localhost"))
            {
                options = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    IsEssential = true,
                    Expires = DateTime.Now.AddMinutes(3)
                };
                options.Domain = "saint-gobain.com";
                Response.Cookies.Append(tokenKey, token, options);
            }
            else
            {
                options = new CookieOptions
                {
                    HttpOnly = false,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    IsEssential = true,
                    Expires = DateTime.Now.AddMinutes(3)
                };
                Response.Cookies.Append(tokenKey, token, options);
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginUserModel LoginUserModel)
        {
            try
            {
                DataSet ds = _actionCtrl.Login(LoginUserModel);
                if (ds == null)
                {
                    var responseValue = new { status = "Failed", response = "", message = "No Record Found" };
                    return Ok(responseValue);
                }
                else if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "success")
                    {
                        var responseValue = new { status = "success", response = ds, message = "" };
                        return Ok(responseValue);
                    }
                    else
                    {
                        var responseValue = new { status = "information", response = "", message = "Invalid data." };
                        return Ok(responseValue);
                    }
                }
                else
                {
                    var responseValue = new { status = "information", response = "", message = "Invalid data." };
                    return Ok(responseValue);
                }
            }
            catch (Exception ex)
            {
                var responseValue = new { status = "Error", response = ex.Message, message = "Invalid  data." };
                return Ok(responseValue);
            }

        }
    }
}
