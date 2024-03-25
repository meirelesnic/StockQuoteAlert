using StockQuoteAlert.Domain.Enums;
using StockQuoteAlert.Domain.Interfaces;
using StockQuoteAlert.Domain.Models;

namespace StockQuoteAlert.Application.Service
{
    public class StockQuoteAnalyzedService : IGetStockTransactionAnalysis
    {
        public QuoteAnalysis GetStockTransactionAnalysis(double targetHigh, double targetLow, Stock stock)
        {
            if (stock.RegularMarketPrice > targetHigh)
            {
                return QuoteAnalysis.SELL;
            }
            if (stock.RegularMarketPrice < targetLow)
            {
                return QuoteAnalysis.BUY;
            }
            return QuoteAnalysis.IGNORE;
        }
    }
}
