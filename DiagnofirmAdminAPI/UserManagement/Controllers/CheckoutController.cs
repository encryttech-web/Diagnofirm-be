using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using DiagnofirmAdmin.Control;
using DiagnofirmAdmin.Model;

namespace DiagnofirmAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutControl _ctrl = new();

        // ================= CREATE =================
        [HttpPost]
        [Route("createcheckout")]
        public IActionResult createcheckout(CheckoutModel model)
        {
            try
            {
                DataSet ds = _ctrl.createcheckout(model);

                return Ok(new
                {
                    status = ds.Tables[0].Rows[0][0].ToString(),
                    response = ds
                });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= CREATE =================
        [HttpPost]
        [Route("updatecheckout")]
        public IActionResult updatecheckout(updateCheckoutModel model)
        {
            try
            {
                DataSet ds = _ctrl.updatecheckout(model);

                return Ok(new
                {
                    status = ds.Tables[0].Rows[0][0].ToString(),
                    response = ds
                });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET ALL =================
        [HttpGet]
        [Route("getcheckout")]
        public IActionResult getcheckout()
        {
            try
            {
                DataSet ds = _ctrl.getcheckout();

                if (ds.Tables[0].Rows.Count > 0)
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
        [Route("getcheckoutbyid")]
        public IActionResult getcheckoutbyid(getCheckoutModel model)
        {
            try
            {
                DataSet ds = _ctrl.getcheckoutbyId(model);

                if (ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET BY PRODUCT =================
        [HttpPost]
        [Route("getcheckoutbyproduct")]
        public IActionResult getcheckoutbyproduct(getCheckoutByProductModel model)
        {
            try
            {
                DataSet ds = _ctrl.getcheckoutbyproduct(model);

                if (ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET BY PACKAGE =================
        [HttpPost]
        [Route("getcheckoutbypackage")]
        public IActionResult getcheckoutbypackage(getCheckoutByPackageModel model)
        {
            try
            {
                DataSet ds = _ctrl.getcheckoutbypackage(model);

                if (ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= DELETE =================
        [HttpPost]
        [Route("deletecheckout")]
        public IActionResult deletecheckout(getCheckoutModel model)
        {
            try
            {
                DataSet ds = _ctrl.deletecheckout(model);

                return Ok(new
                {
                    status = ds.Tables[0].Rows[0][0].ToString(),
                    response = ds
                });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET ALL =================
        [HttpGet]
        [Route("getpayment")]
        public IActionResult getpayment()
        {
            try
            {
                DataSet ds = _ctrl.getpayment();

                if (ds.Tables[0].Rows.Count > 0)
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
        [Route("getpaymentbyid")]
        public IActionResult getpaymentbyid(getPaymentModel model)
        {
            try
            {
                DataSet ds = _ctrl.getpaymentbyid(model);

                if (ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET BY Order ID =================
        [HttpPost]
        [Route("getcheckoutbyOrderId")]
        public IActionResult getcheckoutbyOrderId(getCheckoutbyOrderModel model)
        {
            try
            {
                DataSet ds = _ctrl.getcheckoutbyOrderId(model);

                if (ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET ALL ORDER=================
        [HttpGet]
        [Route("getallorder")]
        public IActionResult getallorder()
        {
            try
            {
                DataSet ds = _ctrl.getallorder();

                if (ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET ALL COUNT=================
        [HttpGet]
        [Route("getallcount")]
        public IActionResult getallcount()
        {
            try
            {
                DataSet ds = _ctrl.getallcount();

                if (ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }
       
    }
}