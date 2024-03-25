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

        private readonly string apiKey;
        private readonly string interval;
        private readonly string modules;
        private readonly string range;
        private readonly string baseUrl;

        public StockQuoteAPIRequest(HttpClient httpClient)
        {
            _httpClient = httpClient;

            apiKey = System.Configuration.ConfigurationManager.AppSettings.Get("ApiKey");
            interval = System.Configuration.ConfigurationManager.AppSettings.Get("Interval");
            modules = System.Configuration.ConfigurationManager.AppSettings.Get("Modules");
            range = System.Configuration.ConfigurationManager.AppSettings.Get("Range");
            baseUrl = System.Configuration.ConfigurationManager.AppSettings.Get("BaseUrl");
        }

        public async Task<Stock> GetStock(string symbol)
        {
            try
            {
                string urlRequest = $"{baseUrl}{symbol}?range={range}&interval={interval}&modules={modules}&token={apiKey}";

                HttpResponseMessage response = await _httpClient.GetAsync(urlRequest);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Houve um problema com a requisição: {response.ReasonPhrase})");
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
