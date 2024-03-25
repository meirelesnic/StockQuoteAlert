using StockQuoteAlert.Application.Service;
using StockQuoteAlert.Domain.Enums;
using StockQuoteAlert.Domain.Interfaces;
using StockQuoteAlert.Infrastructure.APIRequest;
using StockQuoteAlert.Infrastructure.Config;
using System.Net.Http;
using System.Net.Mail;

namespace StockQuoteAlert.Application
{
    public class StockQuoteMonitoring
    {
        private StockQuoteAPIRequest _stockQuoteAPIRequest;
        private readonly HttpClient _httpClient;
        private ISendEmail _emailService;
        private IGetStockTransactionAnalysis _getStockTransactionAnalysis;

        QuoteAnalysis previousQuote = QuoteAnalysis.UNKNOWN;

        public StockQuoteMonitoring(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public void StartMonitoring(double targetHigh, double targetLow, string symbol)
        {
            _getStockTransactionAnalysis = new StockQuoteAnalyzedService();

            _stockQuoteAPIRequest = new StockQuoteAPIRequest(_httpClient);

            var stock = _stockQuoteAPIRequest.GetStock(symbol).Result;

            var stockQuoteAnalyzed = _getStockTransactionAnalysis.GetStockTransactionAnalysis(targetHigh, targetLow, stock);

            var smtpClient = EmailConfig.EmailConfigSMTP();

            _emailService = new SendEmailAlertService(smtpClient);

            var mailMessage = EmailConfig.EmailBody(symbol);

            QuoteAlert(mailMessage, stockQuoteAnalyzed, symbol);

        }

        private void QuoteAlert(MailMessage mailMessage, QuoteAnalysis stockQuoteAnalyzed, string symbol)
        {
            if (stockQuoteAnalyzed != previousQuote)
            {
                previousQuote = stockQuoteAnalyzed;

                if (stockQuoteAnalyzed == QuoteAnalysis.SELL)
                {
                    mailMessage.Body = $"{symbol} está valorizado no mercado! É uma boa oportunidade para vendê-lo!";
                    _emailService.SendEmail(mailMessage);
                }

                if (stockQuoteAnalyzed == QuoteAnalysis.BUY)
                {
                    mailMessage.Body = $"O valor de {symbol} abaixou! É uma boa oportunidade para comprá-lo!";
                    _emailService.SendEmail(mailMessage);
                }
            }
        }
    }
}