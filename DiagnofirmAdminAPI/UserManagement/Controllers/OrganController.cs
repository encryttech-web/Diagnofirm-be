using DiagnofirmAdmin.Control;
using DiagnofirmAdmin.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace DiagnofirmAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganController : ControllerBase
    {
        private readonly OrganControl _actionCtrl;

        public OrganController()
        {
            _actionCtrl = new OrganControl();
        }

        // ================= GET =================
        [HttpPost]
        [Route("getorgan")]
        public IActionResult getorgan(organModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.getorgan(model);

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
        [Route("getorganbyId")]
        public IActionResult getorganbyId(getorganModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.getorganbyId(model);

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
        [Route("addorgan")]
        public IActionResult addorgan(addorganModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.addorgan(model);

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
        [Route("updateorgan")]
        public IActionResult updateorgan(updateorganModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.updateorgan(model);

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
        [Route("delorgan")]
        public IActionResult delorgan(delorganModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.delorgan(model);

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
        public IActionResult getbycategoryandsubcategory(getorganbycatModel model)
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