using StockQuoteAlert.Domain.Enums;
using StockQuoteAlert.Domain.Interfaces;
using StockQuoteAlert.Infrastructure.APIRequest;
using StockQuoteAlert.Infrastructure.Config;

namespace StockQuoteAlert.Application
{
    public class StockQuoteMonitoring
    {
        private StockQuoteAPIRequest _stockQuoteAPIRequest;
        private ISendEmail _emailService;
        private IGetStockTransactionAnalysis _getStockTransactionAnalysis;
        private QuoteAnalysis _previousQuote;

        public StockQuoteMonitoring(StockQuoteAPIRequest stockQuoteAPIRequest,
            ISendEmail emailService, IGetStockTransactionAnalysis getStockTransactionAnalysis)
        {
            _stockQuoteAPIRequest = stockQuoteAPIRequest;
            _emailService = emailService;
            _getStockTransactionAnalysis = getStockTransactionAnalysis;
            _previousQuote = QuoteAnalysis.UNKNOWN;
        }

        public void StartMonitoring(double targetHigh, double targetLow, string symbol)
        {
            var stock = _stockQuoteAPIRequest.GetStock(symbol).Result;

            var stockQuoteAnalyzed = _getStockTransactionAnalysis.GetStockTransactionAnalysis(targetHigh, targetLow, stock);

            QuoteAlert(stockQuoteAnalyzed, symbol);
        }

        private void QuoteAlert(QuoteAnalysis stockQuoteAnalyzed, string symbol)
        {
            var mailMessage = EmailConfig.EmailBody(symbol);

            if (stockQuoteAnalyzed != _previousQuote)
            {
                _previousQuote = stockQuoteAnalyzed;

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