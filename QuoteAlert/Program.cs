using Microsoft.Extensions.DependencyInjection;
using QuoteAlert.Application.Decorator;
using StockQuoteAlert.Application;
using StockQuoteAlert.Application.Decorator;
using StockQuoteAlert.Application.Service;
using StockQuoteAlert.Domain.Interfaces;
using StockQuoteAlert.Infrastructure.APIRequest;
using StockQuoteAlert.Infrastructure.Config;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;

namespace QuoteAlert
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddTransient<StockQuoteMonitoring>()
            .AddTransient<HttpClient>()
            .AddTransient(service =>
            {
                return EmailConfig.EmailConfigSMTP();
            })
            .AddTransient<StockQuoteAPIRequest>()
            .AddTransient<ISendEmail, SendEmailAlertService>()
            .AddTransient<IGetStockTransactionAnalysis, StockQuoteAnalyzedService>()
            .BuildServiceProvider();

            IVerifier verifier = new VerifyInput();

            IVerifier verifierDecorator = new VerifyParametersLengthDecorator(
                                                new VerifySpecialCharacterDecorator(
                                                    new VerifyNumericNumbersDecorator(verifier)));

            if (verifierDecorator.Verify(args))
            {
                string symbol = args[0].ToUpper();
                Double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double targetHigh);
                Double.TryParse(args[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double targetLow);

                var stockQuoteMonitoring = serviceProvider.GetService<StockQuoteMonitoring>();

                while (true)
                {
                    stockQuoteMonitoring.StartMonitoring(targetHigh, targetLow, symbol);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}