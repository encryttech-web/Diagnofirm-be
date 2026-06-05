using DiagnofirmAdmin.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace DiagnofirmAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IConfiguration _config;

        public EmailController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("SendOrderEmail")]
        public IActionResult SendOrderEmail([FromBody] EmailRequest request)
        {
            if (string.IsNullOrEmpty(request.To) ||
                string.IsNullOrEmpty(request.Subject) ||
                string.IsNullOrEmpty(request.Html))
            {
                return BadRequest(new { status = "error", message = "Missing required fields." });
            }

            try
            {
                var gmailUser = _config["Gmail:Username"];
                var gmailPass = _config["Gmail:AppPassword"];

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 465,
                    Credentials = new NetworkCredential(gmailUser, gmailPass),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(gmailUser!, "Diagnofirm Orders"),
                    Subject = request.Subject,
                    Body = request.Html,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(request.To);

                smtpClient.Send(mailMessage);

                return Ok(new { status = "success", message = "Email sent." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }
    }
}
