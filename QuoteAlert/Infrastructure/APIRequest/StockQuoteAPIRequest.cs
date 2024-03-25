using StockQuoteAlert.Domain.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StockQuoteAlert.Infrastructure.APIRequest
{
    public class StockQuoteAPIRequest
    {
        private readonly HttpClient _httpClient;

        private readonly string apiKey = System.Configuration.ConfigurationManager.AppSettings.Get("ApiKey");
        private readonly string interval = System.Configuration.ConfigurationManager.AppSettings.Get("Interval");
        private readonly string modules = System.Configuration.ConfigurationManager.AppSettings.Get("Modules");
        private readonly string range = System.Configuration.ConfigurationManager.AppSettings.Get("Range");
        private readonly string baseUrl= System.Configuration.ConfigurationManager.AppSettings.Get("BaseUrl");

        public StockQuoteAPIRequest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Stock> GetStock(string symbol)
        {
            try
            {
                string urlRequest = $"{baseUrl}{symbol}?range={range}&interval={interval}&modules={modules}&token={apiKey}";

                HttpResponseMessage response = await _httpClient.GetAsync(urlRequest);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Aconteceu um problema. {response.StatusCode}");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonData = JsonSerializer.Deserialize<StockQuoteResponse>(jsonResponse);

                return JsonSerializer.Deserialize<Stock>(jsonData.Results[0]);

            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
