using DiagnofirmAdmin.Control;
using DiagnofirmAdmin.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DiagnofirmAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaqController : ControllerBase
    {
        private readonly FaqControl _faqCtrl;

        public FaqController()
        {
            _faqCtrl = new FaqControl();
        }

        // ================= GET ALL FAQ =================
        [HttpGet]
        [Route("getfaq")]
        public IActionResult getfaq()
        {
            try
            {
                DataSet ds = _faqCtrl.getfaq();

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

        // ================= GET FAQ BY ID =================
        [HttpPost]
        [Route("getfaqbyId")]
        public IActionResult getfaqbyId(getfaqModel model)
        {
            try
            {
                DataSet ds = _faqCtrl.getfaqbyId(model);

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", response = "", message = "Invalid data" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET FAQ BY PackageID =================
        [HttpPost]
        [Route("getfaqbypackageId")]
        public IActionResult getfaqbypackageId(getfaqpackageModel model)
        {
            try
            {
                DataSet ds = _faqCtrl.getfaqbypackageId(model);

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", response = "", message = "Invalid data" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET FAQ BY SubcategoryID =================
        [HttpPost]
        [Route("getfaqbysubcatId")]
        public IActionResult getfaqbysubcatId(getfaqsubcatModel model)
        {
            try
            {
                DataSet ds = _faqCtrl.getfaqbysubcatId(model);

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", response = "", message = "Invalid data" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET FAQ BY ProductID =================
        [HttpPost]
        [Route("getfaqbyproductId")]
        public IActionResult getfaqbyproductId(getfaqproductModel model)
        {
            try
            {
                DataSet ds = _faqCtrl.getfaqbyproductId(model);

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", response = "", message = "Invalid data" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET FAQ BY Homecheck =================
        [HttpGet]
        [Route("getfaqbyhomecheck")]
        public IActionResult getfaqbyhomecheck()
        {
            try
            {
                DataSet ds = _faqCtrl.getfaqbyhomecheck();

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", response = "", message = "Invalid data" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= ADD FAQ =================
        [HttpPost]
        [Route("addfaq")]
        public IActionResult addfaq(addfaqModel model)
        {
            try
            {
                DataSet ds = _faqCtrl.addfaq(model);

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

        // ================= UPDATE FAQ =================
        [HttpPost]
        [Route("updatefaq")]
        public IActionResult updatefaq(updatefaqModel model)
        {
            try
            {
                DataSet ds = _faqCtrl.updatefaq(model);

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

        // ================= DELETE FAQ =================
        [HttpPost]
        [Route("deletefaq")]
        public IActionResult deletefaq(deletefaqModel model)
        {
            try
            {
                DataSet ds = _faqCtrl.delfaq(model);

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
    }
}