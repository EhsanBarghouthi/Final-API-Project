using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RDP_NTier_Task.DAL.DTO.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using static RDP_NTier_Task.BL.General_Services.EmailSender;

namespace RDP_NTier_Task.BL.General_Services
{
    public class EmailSender : IEmailSender
    {

        private readonly EmailSettings settings;

        public EmailSender(IOptions<EmailSettings> settings)
        {
            this.settings = settings.Value;
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var smtp = new SmtpClient(settings.SmtpServer, settings.Port)
            {
                Credentials = new NetworkCredential(settings.UserName, settings.Password),
                EnableSsl = true
            };

            var mail = new MailMessage(settings.From, to, subject, body)
            {
                IsBodyHtml = true
            };

            await smtp.SendMailAsync(mail);
        }
    }
}
