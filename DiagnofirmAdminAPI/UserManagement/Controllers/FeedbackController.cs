using DiagnofirmAdmin.Control;
using DiagnofirmAdmin.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace DiagnofirmAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackControl _ctrl;

        public FeedbackController()
        {
            _ctrl = new FeedbackControl();
        }

        // ================= GET ALL =================
        [HttpGet]
        [Route("getfeedback")]
        public IActionResult getfeedback()
        {
            try
            {
                DataSet ds = _ctrl.getfeedback();

                if (ds == null)
                    return Ok(new { status = "Failed", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET BY ID =================
        [HttpPost]
        [Route("getfeedbackbyid")]
        public IActionResult getfeedbackbyid(getfeedbackbyidModel model)
        {
            try
            {
                DataSet ds = _ctrl.getfeedbackbyid(model);

                if (ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "failed", message = "Not found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET BY USER =================
        [HttpPost]
        [Route("getfeedbackbyuserid")]
        public IActionResult getfeedbackbyuserid(getfeedbackbyuseridModel model)
        {
            try
            {
                DataSet ds = _ctrl.getfeedbackbyuserid(model);

                if (ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "failed", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= ADD =================
        [HttpPost]
        [Route("addfeedback")]
        public IActionResult addfeedback(addfeedbackModel model)
        {
            try
            {
                DataSet ds = _ctrl.addfeedback(model);
                string result = ds.Tables[0].Rows[0][0].ToString();

                return Ok(new { status = result, response = ds });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= UPDATE =================
        [HttpPost]
        [Route("updatefeedback")]
        public IActionResult updatefeedback(updatefeedbackModel model)
        {
            try
            {
                DataSet ds = _ctrl.updatefeedback(model);
                string result = ds.Tables[0].Rows[0][0].ToString();

                return Ok(new { status = result, response = ds });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= DELETE =================
        [HttpPost]
        [Route("deletefeedback")]
        public IActionResult deletefeedback(deletefeedbackModel model)
        {
            try
            {
                DataSet ds = _ctrl.deletefeedback(model);
                string result = ds.Tables[0].Rows[0][0].ToString();

                return Ok(new { status = result, response = ds });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }
    }
}