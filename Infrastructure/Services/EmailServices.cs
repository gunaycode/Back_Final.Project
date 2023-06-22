using Application.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailServices : IEmailServices
    {

        private readonly IConfiguration _configuration;
        private readonly SmtpClient _smtpClient;

        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(_configuration["Email:From"], _configuration["Email:AppPassword"])
            };
            _smtpClient = smtpClient;
        }
        public void SendMessage(string message, string subject, string to)
        {
            MailMessage newMessage = new MailMessage(_configuration["Email:From"], to)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            _smtpClient.Send(newMessage);
        }

    }

}
