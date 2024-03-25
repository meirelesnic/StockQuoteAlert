using StockQuoteAlert.Domain.Interfaces;
using System.Net.Mail;

namespace StockQuoteAlert.Application.Service
{
    class SendEmailAlertService : ISendEmail
    {
        private readonly SmtpClient _smtpClient;

        public SendEmailAlertService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public void SendEmail(MailMessage mailMessage)
        {
            _smtpClient.Send(mailMessage);
        }
    }
}
