using DiagnofirmAdmin.Control;
using DiagnofirmAdmin.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace DiagnofirmAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackageController : ControllerBase
    {
        private readonly PackageControl _ctrl;

        public PackageController()
        {
            _ctrl = new PackageControl();
        }

        // ================= GET ALL =================
        [HttpGet]
        [Route("getpackage")]
        public IActionResult getpackage()
        {
            try
            {
                DataSet ds = _ctrl.getpackage();

                if (ds == null)
                    return Ok(new { status = "Failed", message = "No Record Found" });

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
        [Route("getpackagebyid")]
        public IActionResult getpackagebyid(getpackagebyidModel model)
        {
            try
            {
                DataSet ds = _ctrl.getpackagebyid(model);

                if (ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "failed", message = "Not found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        // ================= ADD =================
        [HttpPost]
        [Route("addpackage")]
        public async Task<IActionResult> addpackage([FromForm] addpackageModel model)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var files = formCollection.Files;

                if (files.Count > 0)
                {
                    //var imagefiles = formCollection.Files[0];
                    var imageArray = GetImageJsonList(files[0]);
                    model.packageimage = imageArray[0].imageBase64value;

                }

                DataSet ds = _ctrl.addpackage(model);
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
        [Route("updatepackage")]
        public async Task<IActionResult> updatepackage([FromForm] updatepackageModel model)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var files = formCollection.Files;

                if (files.Count > 0)
                {
                    //var imagefiles = formCollection.Files[0];
                    var imageArray = GetImageJsonList(files[0]);
                    model.packageimage = imageArray[0].imageBase64value;

                }

                DataSet ds = _ctrl.updatepackage(model);
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
        [Route("deletepackage")]
        public IActionResult deletepackage(deletepackageModel model)
        {
            try
            {
                DataSet ds = _ctrl.deletepackage(model);
                string result = ds.Tables[0].Rows[0][0].ToString();

                return Ok(new { status = result, response = ds });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        [HttpPost]
        [Route("getpackageImagebyId")]
        public IActionResult getpackageImagebyId(packageimageviewModel packageimageviewModel)
        {
            try
            {
                DataSet ds = _ctrl.getpackageImagebyId(packageimageviewModel);
                List<packageImagevalModel> imageData = new List<packageImagevalModel>();

                if (ds == null)
                {
                    var responseValue = new { status = "Failed", response = "", message = "No Record Found" };
                    return Ok(responseValue);
                }
                else if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "success")
                    {

                        string imgBase64String = ds.Tables[1].Rows[0].ItemArray[1].ToString();
                        string imagename = ds.Tables[1].Rows[0].ItemArray[2].ToString();
                        string imagepath = ds.Tables[1].Rows[0].ItemArray[2].ToString();
                        string packageId = ds.Tables[1].Rows[0].ItemArray[0].ToString();

                        imageData.Add(new packageImagevalModel
                        {
                            imagenamevalue = imagename.ToString(),
                            imagepathvalue = imagepath.ToString(),
                            packageId = packageId.ToString(),
                            imageBase64value = "data:image/png;base64," + imgBase64String,
                        });


                        var responseValue = new { status = "success", response = imageData, message = "" };
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
        [Route("SearchPackages")]
        public IActionResult SearchPackages([FromBody] SearchRequestModel request)
        {
            try
            {
                DataSet ds = _ctrl.SearchPackages(request.Query);
                if (ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });
                return Ok(new { status = "information", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        private List<ImageJsonModel> GetImageJsonList(Microsoft.AspNetCore.Http.IFormFile file)
        {
            List<ImageJsonModel> imageArray = new();

            if (file != null)
            {
                string base64String;

                var fileName = file.FileName;
                var fileType = file.ContentType;

                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    byte[] imageBytes = stream.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);

                    imageArray.Add(new ImageJsonModel
                    {
                        imagenamevalue = fileName,
                        imagepathvalue = fileName,
                        imageTypeValue = fileType,
                        imageBase64value = base64String
                    });
                }
            }

            return imageArray;
        }

    }
}