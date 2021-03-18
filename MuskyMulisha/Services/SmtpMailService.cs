using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MuskyMulisha.Models;

namespace MuskyMulisha.Services
{
    public class SmtpMailService : IMailService
    {
        
        private readonly NetworkCredential _credentials;
        
        public SmtpMailService()
        {
            _credentials = new NetworkCredential("bcker08@gmail.com", Environment.GetEnvironmentVariable("EMAIL_PASSWORD"));
        }

        public async Task<EmailStatus> SendAsync(EmailModel emailModel)
        {
            try
            {
                using var smtpClient = new SmtpClient
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = _credentials
                };

                var mailMessage = new MailMessage
                {
                    Body = $"User: {emailModel.FullName ?? "<Name Not Provided>"}\n\n" +
                           $"Message: {emailModel.Message ?? "<Message Not Provided>"}\n\n" +
                           $"PhoneNumber: {emailModel.PhoneNumber ?? "<Phone Not Provided>"}\n\n" +
                           $"Email Address: {emailModel.EmailAddress ?? "<Email Not Provided>"}",
                    Subject = $"User {emailModel.FullName ?? "<Name Not Provided>"} has sent you an email.",
                    From = new MailAddress("noreply@MuskyMulisha.com")
                };
                //msg.To.Add("MuskyMulisha@gmail.com");
                mailMessage.To.Add("me@evanbecker.com");
                await smtpClient.SendMailAsync(mailMessage);

                return new EmailStatus
                {
                    StatusEnum = EmailStatusEnum.Success,
                    Message = "Email sent successfully!"
                };
            }
            catch (Exception)
            {
                return new EmailStatus
                {
                    StatusEnum = EmailStatusEnum.Error,
                    Message = "Email is not successful. Please manually email error to MuskyMulisha@gmail.com!"
                };
            }
        }
    }
}