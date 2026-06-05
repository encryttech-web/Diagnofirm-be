using System;
using System.Collections.Generic;
using System.Data;
using ORIONAPI.Control;
using ORIONAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ORIONAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  
    public class ErrorLogController : ControllerBase
    {
        private readonly ErrorlogModel _logger = new ErrorlogModel();
        private readonly LogControl _Logcontrol = new LogControl();
        private readonly ErrorLogControl _actionCtrl = new ErrorLogControl();

        [HttpPost]
        [Route("InsertErrorLogDetails")]
        public IActionResult InsertErrorLogDetails([FromBody]JToken Message)
        {
            try
            {
                _Logcontrol.InsertLogDetails(Request.Headers["Authorization"]);

                ErrorLogModel _usermodel = JsonConvert.DeserializeObject<List<ErrorLogModel>>(Message.ToString())[0];

                DataSet dt;
                dt = _actionCtrl.InsertErrorLogDetails(_usermodel);

                if (dt.Tables[0].Rows.Count > 0)
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