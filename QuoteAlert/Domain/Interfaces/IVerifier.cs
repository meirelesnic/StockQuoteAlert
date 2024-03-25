namespace StockQuoteAlert.Domain.Interfaces
{
    public interface IVerifier
    {
        bool Verify(string[] args);
    }
}
