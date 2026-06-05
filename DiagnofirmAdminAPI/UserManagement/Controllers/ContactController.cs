using DiagnofirmAdmin.Control;
using DiagnofirmAdmin.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace DiagnofirmAdmin.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ContactControl _actionCtrl;

        public ContactController()
        {
            _actionCtrl = new ContactControl();
        }

        // ================= GET ALL CONTACTS =================
        [HttpGet]
        [Route("getcontact")]
        public IActionResult getcontact()
        {
            try
            {
                DataSet ds = _actionCtrl.getcontact();

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds, message = "" });

                return Ok(new { status = "information", response = "", message = "Invalid data" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message, message = "" });
            }
        }

        // ================= GET BY ID =================
        [HttpPost]
        [Route("getcontactbyId")]
        public IActionResult getcontactbyId(getcontactModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.getcontactbyId(model);

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds, message = "" });

                return Ok(new { status = "information", response = "", message = "Invalid data" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message, message = "" });
            }
        }

        // ================= ADD =================
        [HttpPost]
        [Route("addcontact")]
        public IActionResult addcontact(addcontactModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.addcontact(model);

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
        [Route("updatecontact")]
        public IActionResult updatecontact(updatecontactModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.updatecontact(model);

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
        [Route("delcontact")]
        public IActionResult delcontact(delcontactModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.delcontact(model);

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

        // ================= SEARCH =================
        [HttpPost]
        [Route("SearchContact")]
        public IActionResult SearchContact([FromBody] SearchContactRequestModel request)
        {
            try
            {
                DataSet ds = _actionCtrl.SearchContact(request.Query);

                if (ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= GET ALL CONTACTS =================
        [HttpGet]
        [Route("getcontacttype")]
        public IActionResult getcontacttype()
        {
            try
            {
                DataSet ds = _actionCtrl.getcontacttype();

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds, message = "" });

                return Ok(new { status = "information", response = "", message = "Invalid data" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message, message = "" });
            }
        }

        // ================= GET BY ID =================
        [HttpPost]
        [Route("getcontacttypebyId")]
        public IActionResult getcontacttypebyId(getcontacttypeModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.getcontacttypebyId(model);

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds, message = "" });

                return Ok(new { status = "information", response = "", message = "Invalid data" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message, message = "" });
            }
        }
    }
}