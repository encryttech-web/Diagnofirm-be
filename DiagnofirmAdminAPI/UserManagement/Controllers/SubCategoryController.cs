using DiagnofirmAdmin.Control;
using DiagnofirmAdmin.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace DiagnofirmAdmin.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly SubCategoryControl _actionCtrl;

        public SubCategoryController()
        {
            _actionCtrl = new SubCategoryControl();
        }

        [HttpGet]
        [Route("getsubcategory")]
        public IActionResult getsubcategory()
        {
            try
            {
                DataSet ds = _actionCtrl.getsubcategory();

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

        [HttpPost]
        [Route("getsubcategorybyId")]
        public IActionResult getsubcategorybyId(getsubcategoryModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.getsubcategorybyId(model);

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

        [HttpPost]
        [Route("addsubcategory")]
        public async Task<IActionResult> addsubcategory([FromForm] addsubcategoryModel model)
        {
            try
            {

                var formCollection = await Request.ReadFormAsync();
                var files = formCollection.Files;

                if (files.Count > 0)
                {
                    //var imagefiles = formCollection.Files[0];
                    var imageArray = GetImageJsonList(files[0]);
                    model.subcategoryimage = imageArray[0].imageBase64value;

                }

                DataSet ds = _actionCtrl.addsubcategory(model);

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
        [Route("updatesubcategory")]
        public async Task<IActionResult> updatesubcategory([FromForm] updatesubcategoryModel model)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var files = formCollection.Files;

                if (files.Count > 0)
                {
                    //var imagefiles = formCollection.Files[0];
                    var imageArray = GetImageJsonList(files[0]);
                    model.subcategoryimage = imageArray[0].imageBase64value;

                }

                DataSet ds = _actionCtrl.updatesubcategory(model);

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
        [Route("delsubcategory")]
        public IActionResult delsubcategory(delsubcategoryModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.delsubcategory(model);

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

        // ================= GET BY CATEGORY ID =================
        [HttpPost]
        [Route("getsubcategorybycategoryid")]
        public IActionResult getsubcategorybycategoryid(getsubcategorybycategoryModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.getsubcategorybycategoryid(model);

                if (ds == null)
                    return Ok(new { status = "Failed", message = "No Record Found" });

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        [HttpPost]
        [Route("getsubcatImagebyId")]
        public IActionResult getsubcatImagebyId(subcatimageviewModel subcatimageviewModel)
        {
            try
            {
                DataSet ds = _actionCtrl.getsubcatImagebyId(subcatimageviewModel);
                List<ImagesubcatvalModel> imageData = new List<ImagesubcatvalModel>();

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
                        string subcategoryId = ds.Tables[1].Rows[0].ItemArray[0].ToString();

                        imageData.Add(new ImagesubcatvalModel
                        {
                            imagenamevalue = imagename.ToString(),
                            imagepathvalue = imagepath.ToString(),
                            subcategoryId = subcategoryId.ToString(),
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
        [Route("SearchSubcategory")]
        public IActionResult SearchSubcategory([FromBody] SearchRequestModel request)
        {
            try
            {
                DataSet ds = _actionCtrl.SearchSubcategory(request.Query);
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