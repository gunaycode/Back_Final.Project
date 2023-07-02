using Application.DTOs.MailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IEmailServices
    {
        void SendMessage(string message, string subject, string to);
        Task SendMessage(MailRequestDto mailRequestDto);
    }
}
