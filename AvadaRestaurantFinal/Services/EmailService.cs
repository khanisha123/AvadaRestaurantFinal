using AvadaRestaurantFinal.Services.Interfaces;
using AvadaRestaurantFinal.Utilities.Helper;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Services
{
    public class EmailService : IEmailServices
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string emailTo,string userName,string html,string content)
        {
            var emailModel = _config.GetSection("EmailConfig").Get<EmailRequest>();
            var apiKey = emailModel.SecretKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(emailModel.SenderEmail,emailModel.SenderName);
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(emailTo, userName);
            var plainTextContent = content;
            var htmlContent = html;
            var msg = MailHelper.CreateSingleEmail(from,to,subject,plainTextContent,htmlContent);
            await client.SendEmailAsync(msg);
        }
        //$"<a href={url}>Click here</a>"
    }
}
