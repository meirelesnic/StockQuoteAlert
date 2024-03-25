using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace StockQuoteAlert.Domain.Models
{
    public class StockQuoteResponse
    {
        [JsonProperty("results")]
        [JsonPropertyName("results")]
        public dynamic Results { get; set; }
    }
}
