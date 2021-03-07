using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MuskyMulisha.Models;

namespace MuskyMulisha.Services
{
    public class SmtpMailService : IMailService
    {
        public SmtpMailService()
        {
            Credentials = new NetworkCredential("bcker08@gmail.com", Environment.GetEnvironmentVariable("EMAIL_PASSWORD"));
        }

        public NetworkCredential Credentials { get; set; }
        
        public async Task<EmailStatus> SendAsync(EmailModel emailModel)
        {
            try
            {
                using var smtp = new SmtpClient
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = Credentials
                };
                var msg = new MailMessage
                {
                    Body = $"User: {emailModel.FullName}\n\n" +
                           $"Message: {emailModel.Message}\n\n" +
                           $"PhoneNumber: {emailModel.PhoneNumber}\n\n" +
                           $"Email Address: {emailModel.EmailAddress}",
                    Subject = $"User {emailModel.FullName} has sent you an email.",
                    From = new MailAddress("noreply@MuskyMulisha.com")
                };
                //msg.To.Add("MuskyMulisha@gmail.com");
                msg.To.Add("me@evanbecker.com");
                await smtp.SendMailAsync(msg);
                return new EmailStatus
                {
                    Status = ErrorStatus.Success,
                    Message = "Email sent successfully!"
                };;
            }
            catch (Exception e)
            {
                return new EmailStatus
                {
                    Status = ErrorStatus.Error,
                    Message = $"Email is not successful. Please manually email error to MuskyMulisha@gmail.com, " +
                              $"and include the error message:\n" +
                              $"{e.Message}"
                };
            }
        }
    }
}