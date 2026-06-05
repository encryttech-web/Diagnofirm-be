using DiagnofirmAdmin.Control;
using DiagnofirmAdmin.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace DiagnofirmAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestDirectoryController : ControllerBase
    {
        private readonly TestDirectoryControl _actionCtrl;

        public TestDirectoryController()
        {
            _actionCtrl = new TestDirectoryControl();
        }

        // ================= GET =================
        [HttpGet]
        [Route("gettestdirectory")]
        public IActionResult gettestdirectory()
        {
            try
            {
                DataSet ds = _actionCtrl.gettestdirectory();

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", response = "", message = "Invalid data." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET BY ID =================
        [HttpPost]
        [Route("gettestdirectorybyId")]
        public IActionResult gettestdirectorybyId(gettestdirectoryModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.gettestdirectorybyId(model);

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", response = "", message = "Invalid data." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= ADD =================
        [HttpPost]
        [Route("addtestdirectory")]
        public IActionResult addtestdirectory(addtestdirectoryModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.addtestdirectory(model);

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
        [Route("updatetestdirectory")]
        public IActionResult updatetestdirectory(updatetestdirectoryModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.updatetestdirectory(model);

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
        [Route("deltestdirectory")]
        public IActionResult deltestdirectory(deltestdirectoryModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.deltestdirectory(model);

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

        // ================= GET =================
        [HttpGet]
        [Route("gettestdirectoryIndustry")]
        public IActionResult gettestdirectoryIndustry()
        {
            try
            {
                DataSet ds = _actionCtrl.gettestdirectoryIndustry();

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", response = "", message = "Invalid data." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET BY ID =================
        [HttpPost]
        [Route("gettestdirectorybyIndustryId")]
        public IActionResult gettestdirectorybyIndustryId(gettestdirectoryIndustryModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.gettestdirectorybyIndustryId(model);

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", response = "", message = "Invalid data." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }
    }
}