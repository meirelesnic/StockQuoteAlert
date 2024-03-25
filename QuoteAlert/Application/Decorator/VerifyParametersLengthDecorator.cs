using StockQuoteAlert.Domain.Interfaces;
using System;

namespace StockQuoteAlert.Application.Decorator
{
    public class VerifyParametersLengthDecorator : VerifyDecorator
    {
        private const int PARAMS_LENGTH = 3;

        public VerifyParametersLengthDecorator(IVerifier verifier) : base(verifier)
        {
        }

        public override bool Verify(string[] args)
        {
            if (args.Length != PARAMS_LENGTH)
            {
                Console.WriteLine("Uso incorreto. Você deve inserir o Ticker e dois valores de referência.");
                Console.WriteLine("Exemplo: QuoteAlert.exe PETR4 22.67 22.59");
                return false;
            }
            return base.Verify(args);
        }
    }
}
