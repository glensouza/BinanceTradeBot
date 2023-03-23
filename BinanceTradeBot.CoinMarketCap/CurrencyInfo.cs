using Newtonsoft.Json;

namespace BinanceTradeBot.CoinMarketCap
{
    public class CurrencyInfo
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "token_address")]
        public string TokenAddress { get; set; } = string.Empty;
    }
}
