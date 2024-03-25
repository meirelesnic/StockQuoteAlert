using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace StockQuoteAlert.Domain.Models
{
    public class Stock
    {
        [JsonProperty("currency")]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonProperty("shortName")]
        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("longName")]
        [JsonPropertyName("longName")]
        public string LongName { get; set; }

        [JsonProperty("regularMarketPrice")]
        [JsonPropertyName("regularMarketPrice")]
        public double RegularMarketPrice { get; set; }
    }
}
