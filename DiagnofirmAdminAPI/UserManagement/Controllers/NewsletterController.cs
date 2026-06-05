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
    public class NewsletterController : ControllerBase
    {
        private readonly NewsletterControl _ctrl;

        public NewsletterController()
        {
            _ctrl = new NewsletterControl();
        }

        // ================= GET ALL =================
        [HttpGet]
        [Route("getnewsletter")]
        public IActionResult getnewsletter()
        {
            try
            {
                DataSet ds = _ctrl.getnewsletter();

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

        // ================= GET BY ID =================
        [HttpPost]
        [Route("getnewsletterbyid")]
        public IActionResult getnewsletterbyid(getnewsletterbyidModel model)
        {
            try
            {
                DataSet ds = _ctrl.getnewsletterbyid(model);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
        [Route("addnewsletter")]
        public async Task<IActionResult> addnewsletter([FromForm] addnewsletterModel model)
        {
            try
            {

                var formCollection = await Request.ReadFormAsync();
                //Image
                var files = formCollection.Files;

                if (files.Count > 0)
                {
                    var imageArray = GetImageJsonList(files[0]);
                    model.letter_image = imageArray[0].imageBase64value;
                }

                // FILE
                var uploadFile = formCollection.Files["letter_file"];

                if (uploadFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await uploadFile.CopyToAsync(ms);

                        byte[] fileBytes = ms.ToArray();

                        // STORE BASE64 STRING
                        model.letter_file = Convert.ToBase64String(fileBytes);

                        model.letter_filename = uploadFile.FileName;
                    }
                }

                DataSet ds = _ctrl.addnewsletter(model);
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
        [Route("updatenewsletter")]
        public async Task<IActionResult> updatenewsletter([FromForm] updatenewsletterModel model)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();

                // IMAGE
                var imageFile = formCollection.Files["letterimage"];

                if (imageFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(ms);
                        model.letterimage = Convert.ToBase64String(ms.ToArray());
                        model.letterimgname = imageFile.FileName;
                    }
                }

                // FILE (PDF)
                var uploadFile = formCollection.Files["letterfile"];

                if (uploadFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await uploadFile.CopyToAsync(ms);
                        model.letterfile = Convert.ToBase64String(ms.ToArray());
                        model.letterfilename = uploadFile.FileName;
                    }
                }
                ////Image
                //var files = formCollection.Files;

                //if (files.Count > 0)
                //{
                //    var imageArray = GetImageJsonList(files[0]);
                //    model.letter_image = imageArray[0].imageBase64value;
                //}

                //// FILE
                //var uploadFile = formCollection.Files["letter_file"];

                //if (uploadFile != null)
                //{
                //    using (var ms = new MemoryStream())
                //    {
                //        await uploadFile.CopyToAsync(ms);

                //        byte[] fileBytes = ms.ToArray();

                //        // STORE BASE64 STRING
                //        model.letter_file = Convert.ToBase64String(fileBytes);

                //        model.letter_filename = uploadFile.FileName;
                //    }
                //}

                DataSet ds = _ctrl.updatenewsletter(model);
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
        [Route("deletenewsletter")]
        public IActionResult deletenewsletter(deletenewsletterModel model)
        {
            try
            {
                DataSet ds = _ctrl.deletenewsletter(model);
                string result = ds.Tables[0].Rows[0][0].ToString();

                return Ok(new { status = result, response = ds });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", response = ex.Message });
            }
        }

        [HttpPost]
        [Route("getnewsletterImagebyId")]
        public IActionResult getnewsletterImagebyId(newsletterimageviewModel newsletterimageviewModel)
        {
            try
            {
                DataSet ds = _ctrl.getnewsletterImagebyId(newsletterimageviewModel);
                List<newsletterImagevalModel> imageData = new List<newsletterImagevalModel>();

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
                        string newsletterId = ds.Tables[1].Rows[0].ItemArray[0].ToString();
                        string fileBase64String = ds.Tables[1].Rows[0].ItemArray[3].ToString();
                        string filename = ds.Tables[1].Rows[0].ItemArray[4].ToString();

                        imageData.Add(new newsletterImagevalModel
                        {
                            imagenamevalue = imagename.ToString(),
                            imagepathvalue = imagepath.ToString(),
                            newsletterId = newsletterId.ToString(),
                            imageBase64value = "data:image/png;base64," + imgBase64String,
                            filename = filename.ToString(),
                            fileBase64String = "data:image/png;base64," + fileBase64String,
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