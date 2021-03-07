using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MuskyMulisha.Models;
using MuskyMulisha.Services;

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
        
        public async Task OnPost()
        {
            EmailStatus = await _mailService.SendAsync(EmailModel);
        }
    }
}
