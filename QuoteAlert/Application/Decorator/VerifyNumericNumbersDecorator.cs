using StockQuoteAlert.Application.Decorator;
using StockQuoteAlert.Domain.Interfaces;
using System;
using System.Globalization;

namespace QuoteAlert.Application.Decorator
{
    public class VerifyNumericNumbersDecorator : VerifyDecorator
    {
        public VerifyNumericNumbersDecorator(IVerifier verifier) : base(verifier)
        {
        }

        public override bool Verify(string[] args)
        {
            if (!(double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double targetHigh) &&
                double.TryParse(args[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double targetLow)))
            {
                Console.WriteLine("Digite apenas números para os valores de referência, sem letras ou caracteres especiais.");
                return false;
            }

            return base.Verify(args);
        }
    }
}
