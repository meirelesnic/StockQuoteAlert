using StockQuoteAlert.Domain.Interfaces;

namespace StockQuoteAlert.Application.Decorator
{
    public abstract class VerifyDecorator : IVerifier
    {
        private readonly IVerifier _verifier;

        public VerifyDecorator(IVerifier verifier)
        {
            _verifier = verifier;
        }

        public virtual bool Verify(string[] args)
        {
            return _verifier.Verify(args);
        }
    }
}
