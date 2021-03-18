using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MuskyMulisha.Models;
using MuskyMulisha.Services;
using Newtonsoft.Json.Linq;

namespace MuskyMulisha.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public EmailModel EmailModel { get; set; }
        public EmailStatus EmailStatus { get; set; }
        
        private readonly ILogger<IndexModel> _logger;
        private readonly IMailService _mailService;

        public ContactModel(ILogger<IndexModel> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        public void OnGet() { }
        
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                string recaptchaResponse = Request.Form["g-recaptcha-response"];
                using var client = new HttpClient();
                var parameters = new Dictionary<string, string>
                {
                    {"secret", Environment.GetEnvironmentVariable("GOOGLE_SECRET")},
                    {"response", recaptchaResponse},
                    {"remoteip", HttpContext.Connection.RemoteIpAddress.ToString()}
                };

                var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(parameters));
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadAsStringAsync();
                dynamic apiJson = JObject.Parse(apiResponse);
                if (apiJson.success != true)
                {
                    EmailStatus = new EmailStatus
                    {
                        StatusEnum = EmailStatusEnum.Error,
                        Message = "Please try again and check the reCAPTCHA."
                    };
                    return Page();
                }
            }
            catch (HttpRequestException)
            {
                EmailStatus = new EmailStatus
                {
                    StatusEnum = EmailStatusEnum.Error,
                    Message = "Unexpected error with reCAPTCHA."
                };
                return Page();
            }
            
            EmailStatus = await _mailService.SendAsync(EmailModel);
            return Page();
        }
    }
}
