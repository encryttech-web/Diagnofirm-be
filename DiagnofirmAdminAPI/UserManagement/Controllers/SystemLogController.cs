using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using ORIONAPI.Model;
using ORIONAPI.Control;
using Microsoft.AspNetCore.Authorization;

namespace ORIONAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SystemLogController : ControllerBase
    {
        public readonly ErrorlogModel _logger = new ErrorlogModel();
        public readonly LogControl _Logcontrol = new LogControl();
        public readonly SystemLogscontrol _actionCtrl = new SystemLogscontrol();

        [Authorize]
        [HttpPost]
        [Route("GetSystemLogUserList")]
        public IActionResult GetSystemLogUserList(SystemLogDeatils _usermodel)
        {
            try
            {
                _Logcontrol.InsertLogDetails(Request.Headers["Authorization"]);

                DataTable dt = _actionCtrl.GetSystemLogUserList(_usermodel);

                if (dt.Rows.Count > 0)
                {
                    var responseValue = new { status = "Success", response = dt, message = "Ok" };
                    return Ok(responseValue);
                } 
                else
                {
                    var responseValue = new { status = "Failed", response = dt, message = "No Data Found" };
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

        [Authorize]
        [HttpPost]
        [Route("GetSystemLogUser")]
        public IActionResult GetSystemLogUser(SystemLogDeatils _usermodel)
        {
            try
            {
                _Logcontrol.InsertLogDetails(Request.Headers["Authorization"]);

                DataTable dt = _actionCtrl.GetUserSystemLog(_usermodel);

                if (dt.Rows.Count > 0)
                {
                    var responseValue = new { status = "Success", response = dt, message = "Ok" };
                    return Ok(responseValue);
                }
                else
                {
                    var responseValue = new { status = "Failed", response = dt, message = "No Data Found" };
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