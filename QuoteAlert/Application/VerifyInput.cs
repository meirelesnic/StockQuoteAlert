using StockQuoteAlert.Domain.Interfaces;
using System;

namespace StockQuoteAlert.Application
{
    public class VerifyInput : IVerifier
    {
        public bool Verify(string[] args)
        {
            double firstValue = double.Parse(args[1]);
            double secondValue = double.Parse(args[2]);

            if (firstValue < 0 || secondValue < 0)
            {
                Console.WriteLine("Os valores de referência não podem ser negativos.");
                return false;
            }

            return true;
        }
    }
}
