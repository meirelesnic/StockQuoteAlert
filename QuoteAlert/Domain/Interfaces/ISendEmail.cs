using System.Net.Mail;

namespace StockQuoteAlert.Domain.Interfaces
{
    public interface ISendEmail
    {
        void SendEmail(MailMessage mailMessage);
    }
}
