using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System;
using DiagnofirmAdmin.Control;
using DiagnofirmAdmin.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SGCrypto;
using System.Collections.Generic;
using System.Linq;
using DiagnofirmAdmin;

namespace DiagnofirmAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsermasterController : ControllerBase
    {
        public readonly UsermasterControl _actionCtrl;
        public readonly EncryptDecrypt _protect = new EncryptDecrypt();
        public UsermasterController(ILogger<UsermasterController> logger)
        {
            _actionCtrl = new UsermasterControl();
        }

        [HttpPost]
        [Route("getuser")]
        public IActionResult getuser(userModel userModel)
        {
            try
            {
                DataSet ds = _actionCtrl.getuser(userModel);
                DecryptUserpassword(ds);
                if (ds == null)
                {
                    var responseValue = new { status = "Failed", response = "", message = "No Record Found" };
                    return Ok(responseValue);
                }
                else if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "success")
                    {
                        //string pwd = _protect.SGDecryption(ds.Tables[0].Rows[0].ItemArray[0].ToString());
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
                var responseValue = new { status = "Error", response = ex.Message, message = "Invalid  data." };
                return Ok(responseValue);
            }

        }

        [HttpPost]
        [Route("getuserbyId")]
        public IActionResult getuserbyId(getuserModel getusermodel)
        {
            try
            {
                DataSet ds = _actionCtrl.getuserbyId(getusermodel);
                DecryptUserpassword(ds);
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
                var responseValue = new { status = "Error", response = ex.Message, message = "Invalid  data." };
                return Ok(responseValue);
            }

        }

        [HttpPost]
        [Route("adduser")]
        public IActionResult adduser(adduserModel adduserModel)
        {
            try
            {
                adduserModel.password = _protect.SGEncryption(adduserModel.password.ToString());
                DataSet ds = _actionCtrl.adduser(adduserModel);
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
                    else if (ds.Tables[0].Rows[0][0].ToString() == "information")
                    {
                        var responseValue = new { status = "information", response = ds, message = "" };
                        return Ok(responseValue);
                    }
                    else
                    {
                        var responseValue = new { status = "failed", response = "", message = "Invalid data." };
                        return Ok(responseValue);
                    }
                }
                else
                {
                    var responseValue = new { status = "failed", response = "", message = "Invalid data." };
                    return Ok(responseValue);
                }
            }
            catch (Exception ex)
            {
                var responseValue = new { status = "Error", response = ex.Message, message = "Invalid  data." };
                return Ok(responseValue);
            }

        }

        [HttpPost]
        [Route("updateuser")]
        public IActionResult updateuser(updateuserModel updateuserModel)
        {
            try
            {
                updateuserModel.password = _protect.SGEncryption(updateuserModel.password.ToString());
                DataSet ds = _actionCtrl.updateuser(updateuserModel);
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
                    else if (ds.Tables[0].Rows[0][0].ToString() == "information")
                    {
                        var responseValue = new { status = "information", response = ds, message = "" };
                        return Ok(responseValue);
                    }
                    else
                    {
                        var responseValue = new { status = "failed", response = "", message = "Invalid data." };
                        return Ok(responseValue);
                    }
                }
                else
                {
                    var responseValue = new { status = "failed", response = "", message = "Invalid data." };
                    return Ok(responseValue);
                }
            }
            catch (Exception ex)
            {
                var responseValue = new { status = "Error", response = ex.Message, message = "Invalid  data." };
                return Ok(responseValue);
            }

        }

        [HttpPost]
        [Route("deluser")]
        public IActionResult deluser(deluserModel deluserModel)
        {
            try
            {
                DataSet ds = _actionCtrl.deluser(deluserModel);
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
                    else if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "failed")
                    {
                        var responseValue = new { status = "failed", response = ds, message = ds.Tables[0].Rows[0][1].ToString() };
                        return Ok(responseValue);
                    }
                    else
                    {
                        var responseValue = new { status = "failed", response = "", message = "Invalid data." };
                        return Ok(responseValue);
                    }
                }
                else
                {
                    var responseValue = new { status = "failed", response = "", message = "Invalid data." };
                    return Ok(responseValue);
                }
            }
            catch (Exception ex)
            {
                var responseValue = new { status = "Error", response = ex.Message, message = "Invalid  data." };
                return Ok(responseValue);
            }

        }

        private void DecryptUserpassword(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "success")
            {
                if (ds.Tables.Count > 1)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        dr[10] = _protect.SGDecryption(dr[10].ToString());
                        ds.AcceptChanges();
                    }
                    if (ds.Tables.Count > 2)
                    {
                        foreach (DataRow dr1 in ds.Tables[2].Rows)
                        {
                            dr1[10] = _protect.SGDecryption(dr1[10].ToString());
                            ds.AcceptChanges();
                        }
                    }
                }
            }
        }

    }
}
