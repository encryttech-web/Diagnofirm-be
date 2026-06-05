using DiagnofirmAdmin.Control;
using DiagnofirmAdmin.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace DiagnofirmAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthConditionController : ControllerBase
    {
        private readonly HealthConditionControl _actionCtrl;

        public HealthConditionController()
        {
            _actionCtrl = new HealthConditionControl();
        }

        // ================= GET =================
        [HttpPost]
        [Route("gethealthcondition")]
        public IActionResult gethealthcondition(healthconditionModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.gethealthcondition(model);

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds, message = "" });

                return Ok(new { status = "information", response = "", message = "Invalid data." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET BY ID =================
        [HttpPost]
        [Route("gethealthconditionbyId")]
        public IActionResult gethealthconditionbyId(gethealthconditionModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.gethealthconditionbyId(model);

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds, message = "" });

                return Ok(new { status = "information", response = "", message = "Invalid data." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= ADD =================
        [HttpPost]
        [Route("addhealthcondition")]
        public IActionResult addhealthcondition(addhealthconditionModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.addhealthcondition(model);

                string result = ds.Tables[0].Rows[0][0].ToString();

                if (result == "success")
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "failed", response = ds });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= UPDATE =================
        [HttpPost]
        [Route("updatehealthcondition")]
        public IActionResult updatehealthcondition(updatehealthconditionModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.updatehealthcondition(model);

                string result = ds.Tables[0].Rows[0][0].ToString();

                if (result == "success")
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "failed", response = ds });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= DELETE =================
        [HttpPost]
        [Route("delhealthcondition")]
        public IActionResult delhealthcondition(delhealthconditionModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.delhealthcondition(model);

                string result = ds.Tables[0].Rows[0][0].ToString();

                if (result == "success")
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "failed", response = ds });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET BY CATEGORY + SUBCATEGORY =================
        [HttpPost]
        [Route("getbycategoryandsubcategory")]
        public IActionResult getbycategoryandsubcategory(gethcbycatModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.getbycategoryandsubcategory(model);

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", response = "", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }
    }
}