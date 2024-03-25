using StockQuoteAlert.Application;
using StockQuoteAlert.Application.Decorator;
using StockQuoteAlert.Domain.Interfaces;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;

namespace QuoteAlert
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //double targetHigh = 60;
            //double targetLow = 50;
            //var symbol = "EGIE3";

            // string[] teste = { null, "PETR4", "20", "3" };

            HttpClient httpClient = new HttpClient();

            IVerifier verifier = new Verifier();

            IVerifier verifierDecorator = new VerifierParametersLengthDecorator(
                                                new VerifierSpecialCharacterDecorator(verifier));

            if (verifierDecorator.Verify(args))
            {
                string symbol = args[0].ToUpper();
                Double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double targetHigh);
                Double.TryParse(args[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double targetLow);

                var sendEmail = new StockQuoteMonitoring(httpClient);

                while (true)
                {
                    sendEmail.StartMonitoring(targetHigh, targetLow, symbol);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}