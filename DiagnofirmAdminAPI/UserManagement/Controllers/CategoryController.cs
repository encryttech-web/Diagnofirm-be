using DiagnofirmAdmin.Control;
using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;

namespace DiagnofirmAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryControl _ctrl;

        public CategoryController()
        {
            _ctrl = new CategoryControl();
        }

        // ================= GET =================

        [HttpGet]
        [Route("getcategory")]
        public IActionResult getcategory()
        {
            try
            {
                DataSet ds = _ctrl.getcategory();

                if (ds == null)
                {
                    var responseValue = new { status = "Failed", response = "", message = "No Record Found" };
                    return Ok(responseValue);
                }
                else if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "success")
                    {
                        var responseValue = new { status = "success", response = ds, message = "" };
                        return Ok(responseValue);
                    }
                    else
                    {
                        var responseValue = new { status = "information", response = "", message = "Invalid data." };
                        return Ok(responseValue);
                    }
                }
                else
                {
                    var responseValue = new { status = "information", response = "", message = "Invalid data." };
                    return Ok(responseValue);
                }
            }
            catch (Exception ex)
            {
                var responseValue = new { status = "Error", response = ex.Message, message = "Invalid data." };
                return Ok(responseValue);
            }
        }

        // ================= GET BY ID =================
        [HttpPost]
        [Route("getcategorybyid")]
        public IActionResult getcategorybyId(getcategoryModel model)
        {
            try
            {
                DataSet ds = _ctrl.getcategorybyId(model);

                if (ds == null)
                    return Ok(new { status = "Failed", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", message = "Invalid data" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= ADD CATEGORY =================
        [HttpPost]
        [Route("addcategory")]
        public IActionResult addcategory(addcategoryModel model)
        {
            try
            {
                DataSet ds = _ctrl.addcategory(model);
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

        // ================= UPDATE CATEGORY =================
        [HttpPost]
        [Route("updatecategory")]
        public IActionResult updatecategory(updatecategoryModel model)
        {
            try
            {
                DataSet ds = _ctrl.updatecategory(model);
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

        // ================= DELETE CATEGORY =================
        [HttpPost]
        [Route("deletecategory")]
        public IActionResult deletecategory(delcategoryModel model)
        {
            try
            {
                DataSet ds = _ctrl.delcategory(model);
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

        [HttpPost]
        [Route("getlastcode")]
        public IActionResult getlastcode(getlastcodeModel model)
        {
            try
            {
                DataSet ds = _ctrl.getlastcode(model);
                string result = ds.Tables[0].Rows[0][1].ToString();

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