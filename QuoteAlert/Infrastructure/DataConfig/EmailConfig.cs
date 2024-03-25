using System.Net;
using System.Net.Mail;
using System.Text;

namespace StockQuoteAlert.Infrastructure.Config
{
    public class EmailConfig
    {
        private static readonly string sender = System.Configuration.ConfigurationManager.AppSettings.Get("Sender");
        private static readonly string recipient = System.Configuration.ConfigurationManager.AppSettings.Get("Recipient");

        public static SmtpClient EmailConfigSMTP()
        {
            return new SmtpClient
            {
                Host = System.Configuration.ConfigurationManager.AppSettings.Get("Host"),
                Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("Port")),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sender, System.Configuration.ConfigurationManager.AppSettings.Get("Password")),
                EnableSsl = true
            };
        }

        public static MailMessage EmailBody(string symbol)
        {
            return new MailMessage(sender, recipient)
            {
                Subject = $"Atualização sobre a cotação de {symbol}",
                IsBodyHtml = true,
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8,
            };
        }
    }
}
