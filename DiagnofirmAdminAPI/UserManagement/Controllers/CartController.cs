using DiagnofirmAdmin.Control;
using DiagnofirmAdmin.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace DiagnofirmAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartControl _ctrl;

        public CartController()
        {
            _ctrl = new CartControl();
        }

        // ================= CREATE =================
        [HttpPost]
        [Route("createcart")]
        public IActionResult createcart(addCartModel model)
        {
            try
            {
                DataSet ds = _ctrl.createcart(model);

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
        [Route("getcart")]
        public IActionResult getcart()
        {
            try
            {
                DataSet ds = _ctrl.getcart();
                return Ok(new { status = "success", response = ds });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET BY ID =================
        [HttpGet]
        [Route("getcartbyid")]
        public IActionResult getcartbyid(getCartModel model)
        {
            try
            {
                DataSet ds = _ctrl.getcartbyid(model);
                return Ok(new { status = "success", response = ds });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= UPDATE =================
        [HttpPost]
        [Route("updatecart")]
        public IActionResult updatecart(CartModel model)
        {
            try
            {
                DataSet ds = _ctrl.updatecart(model);

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

        // ================= DELETE =================
        [HttpPost]
        [Route("deletecart")]
        public IActionResult deletecart(getCartModel model)
        {
            try
            {
                DataSet ds = _ctrl.deletecart(model);

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

        [HttpPost]
        [Route("getcartbyproduct")]
        public IActionResult getcartbyproduct(getCartbyproductModel model)
        {
            try
            {
                DataSet ds = _ctrl.getcartbyproductid(model);

                if (ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET CART BY PACKAGE ID =================
        [HttpPost]
        [Route("getcartbypackage")]
        public IActionResult getcartbypackage(getCartbypackageModel model)
        {
            try
            {
                DataSet ds = _ctrl.getcartbypackageid(model);

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