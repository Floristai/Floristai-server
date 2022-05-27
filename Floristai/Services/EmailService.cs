using Floristai.Models;
using System.Net;
using System.Net.Mail;

namespace Floristai.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(IEmail email, EmailDetails emailDetails)
        {
            var fromAddress = new MailAddress(emailDetails.Email, "Floristai");
            var toAddress = new MailAddress(email.GetRecipientEmail());
            string fromPassword = emailDetails.Password;

            string body = email.GetBody();
            string subject = email.GetSubject();

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                await smtp.SendMailAsync(message);
            }
        }
    }
}
