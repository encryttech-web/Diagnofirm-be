using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using ORIONAPI.Model;
using ORIONAPI.Control;

namespace ORIONAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRightsController : ControllerBase
    {
        public readonly ErrorlogModel _logger = new ErrorlogModel();
        public readonly LogControl log_control = new LogControl();
        public readonly UserRightsControl _actionCtrl = new UserRightsControl();

        [HttpPost]
        [Route("UpdateUserRules")]
        [Authorize]
        public IActionResult UpdateUserRules(UserRights usermodel)
        {
            try
            {           
                log_control.InsertLogDetails(Request.Headers["Authorization"]);
                DataTable dt;
                dt = _actionCtrl.UpdateUserRules(usermodel);
                if (dt.Rows.Count > 0)
                {
                    var responseValue = new { status = "Success", response = dt, message = "" };
                    return Ok(responseValue);
                }
                else
                {
                    var responseValue = new { status = "Failed", response = dt, message = "" };
                    return Ok(responseValue);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(Request.Headers["Authorization"], ex.ToString());
                var responseValue = new { status = "Error", response = "", message = ex.ToString() };
                return Ok(responseValue);
            }
        }

        [HttpGet]
        [Route("GetUser")]
        [Authorize]
        public IActionResult GetUser()
        {
            try
            {
                log_control.InsertLogDetails(Request.Headers["Authorization"]);
                DataTable dt;
                dt = _actionCtrl.GetUser();
                if (dt.Rows.Count > 0)
                {
                    var responseValue = new { status = "Success", response = dt, message = "" };
                    return Ok(responseValue);
                }
                else
                {
                    var responseValue = new { status = "Failed", response = dt, message = "" };
                    return Ok(responseValue);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(Request.Headers["Authorization"], ex.ToString());
                var responseValue = new { status = "Error", response = "", message = ex.ToString() };
                return Ok(responseValue);
            }
        }

        [HttpPost]
        [Route("UserTransaction")]
        [Authorize]
        public IActionResult UserTransaction([FromBody]JToken Message)
        {
            try
            {                
                log_control.InsertLogDetails(Request.Headers["Authorization"]);
                UserRights usermodel;
                usermodel = JsonConvert.DeserializeObject<List<UserRights>>(Message.ToString())[0];                
                DataSet ds;
                ds = _actionCtrl.UserTransaction(usermodel);
                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    var responseValue = new { status = "Success", response = ds, message = "" };
                    return Ok(responseValue);
                }
                else
                {
                    var responseValue = new { status = "Failed", response = ds, message = "" };
                    return Ok(responseValue);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(Request.Headers["Authorization"], ex.ToString());
                var responseValue = new { status = "Error", response = "", message = ex.ToString() };
                return Ok(responseValue);
            }
        }
    }
}