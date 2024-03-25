using StockQuoteAlert.Domain.Interfaces;

namespace StockQuoteAlert.Application.Decorator
{
    public abstract class VerifierDecorator : IVerifier
    {
        private readonly IVerifier _verifier;

        public VerifierDecorator(IVerifier verifier)
        {
            _verifier = verifier;
        }

        public virtual bool Verify(string[] args)
        {
            return _verifier.Verify(args);
        }
    }
}
