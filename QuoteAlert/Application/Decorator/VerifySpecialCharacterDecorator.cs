using StockQuoteAlert.Domain.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace StockQuoteAlert.Application.Decorator
{
    public class VerifySpecialCharacterDecorator : VerifyDecorator
    {
        public VerifySpecialCharacterDecorator(IVerifier verifier) : base(verifier)
        {
        }

        public override bool Verify(string[] args)
        {
            Regex regex = new Regex("^[a-zA-Z0-9,]+$");
            string symbol = args[0].ToUpper();

            if (!regex.IsMatch(symbol))
            {
                Console.WriteLine("O Ticker não pode possuir caracteres especiais.");
                return false;
            }

            return base.Verify(args);
        }
    }
}
