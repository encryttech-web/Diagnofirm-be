using System.Data;
using ORIONAPI.Model;
using ORIONAPI.Control;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;

namespace ORIONAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ErrorController : Controller
    {
        public readonly ErrorModel _usermodel = new ErrorModel();
        public readonly LogControl log_control = new LogControl();
        public readonly ErrorlogModel _logger = new ErrorlogModel();

        [HttpPost]
        [Route("GetError")]
        public IActionResult GetError(Error error)
        {
            try
            {
                log_control.InsertLogDetails(Request.Headers["Authorization"]);
                DataSet dt;
                dt = _usermodel.ErrorDetails(error);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    var responseValue = new { status = "Success", response = dt.Tables[0], message = "" };
                    return Ok(responseValue);
                }
                else
                {
                    var responseValue = new { status = "Failed", response = "", message = "Invalid Label" };
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
        [Route("GetAlertError")]
        public IActionResult GetAlertError(Error error)
        {
            try
            {
                log_control.InsertLogDetails(Request.Headers["Authorization"]);

                DataSet dt;
                dt = _usermodel.ErrorAlertDetails(error);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    var responseValue = new { status = "Success", response = dt.Tables[0], message = "" };
                    return Ok(responseValue);
                }
                else
                {
                    var responseValue = new { status = "Failed", response = "", message = "Invalid Label" };
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