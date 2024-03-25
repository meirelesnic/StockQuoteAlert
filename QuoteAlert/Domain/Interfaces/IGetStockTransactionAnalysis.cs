using StockQuoteAlert.Domain.Enums;
using StockQuoteAlert.Domain.Models;

namespace StockQuoteAlert.Domain.Interfaces
{
    public interface IGetStockTransactionAnalysis
    {
        QuoteAnalysis GetStockTransactionAnalysis(double targetHigh, double targetLow, Stock stock);
    }
}
