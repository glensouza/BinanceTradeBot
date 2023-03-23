using System;
using Newtonsoft.Json;

namespace BinanceTradeBot.CoinMarketCap
{
    public class CurrencyPriceInfo
    {
        [JsonProperty(PropertyName = "price")]
        public double? Price { get; set; }

        [JsonProperty(PropertyName = "volume_24h")]
        public double? Volume24H { get; set; }

        [JsonProperty(PropertyName = "percent_change_1h")]
        public double? PercentChange1H { get; set; }

        [JsonProperty(PropertyName = "percent_change_24h")]
        public double? PercentChange24H { get; set; }

        [JsonProperty(PropertyName = "percent_change_7d")]
        public double? PercentChange7D { get; set; }

        [JsonProperty(PropertyName = "market_cap")]
        public double? MarketCap { get; set; }

        [JsonProperty(PropertyName = "last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}
