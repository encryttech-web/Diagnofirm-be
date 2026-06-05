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
    public class ProductController : ControllerBase
    {
        private readonly ProductControl _actionCtrl;

        public ProductController()
        {
            _actionCtrl = new ProductControl();
        }

        // ================= GET =================
        [HttpGet]
        [Route("getproduct")]
        public IActionResult getproduct()
        {
            try
            {
                DataSet ds = _actionCtrl.getproduct();

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
        [Route("getproductbyId")]
        public IActionResult getproductbyId(getproductModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.getproductbyId(model);

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
        [Route("addproduct")]
        public async Task<IActionResult> addproduct([FromForm] addproductModel model)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var files = formCollection.Files;
                
                if (files.Count > 0)
                {
                    //var imagefiles = formCollection.Files[0];
                    var imageArray = GetImageJsonList(files[0]);
                    model.productimage = imageArray[0].imageBase64value;

                }

                DataSet ds = _actionCtrl.addproduct(model);

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
        [Route("updateproduct")]
        public async Task<IActionResult> updateproduct([FromForm] updateproductModel model)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var files = formCollection.Files;
                //var imagefiles = formCollection.Files[0];

                if (files.Count > 0)
                {
                     var imageArray = GetImageJsonList(files[0]);
                     model.productimage = imageArray[0].imageBase64value;
                }

                DataSet ds = _actionCtrl.updateproduct(model);

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
        [Route("delproduct")]
        public IActionResult delproduct(delproductModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.delproduct(model);

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

        // ================= GET BY CATEGORY =================
        [HttpPost]
        [Route("getbycatid")]
        public IActionResult getbycatid(getproductbycatModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.getbycatid(model);

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

        // ================= CAT + SUBCAT =================
        [HttpPost]
        [Route("getbycatandsubcat")]
        public IActionResult getbycatandsubcat(getproductbycatsubModel model)
        {
            try
            {
                DataSet ds = _actionCtrl.getbycatandsubcat(model);

                if (ds == null)
                    return Ok(new { status = "Failed", response = "", message = "No Record Found" });

                if (ds.Tables.Count > 0)
                    return Ok(new { status = "success", response = ds });

                return Ok(new { status = "information", response = "", message = "No data found" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        [HttpPost]
        [Route("getImagebyId")]
        public IActionResult getImagebyId(imageviewModel imageviewModel)
        {
            try
            {
                DataSet ds = _actionCtrl.getImagebyId(imageviewModel);
                List<ImagevalModel> imageData = new List<ImagevalModel>();

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
                        string productId = ds.Tables[1].Rows[0].ItemArray[0].ToString();

                        imageData.Add(new ImagevalModel
                        {
                            imagenamevalue = imagename.ToString(),
                            imagepathvalue = imagepath.ToString(),
                            productId = productId.ToString(),
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