using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BinanceTradeBot.CoinMarketCap
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "num_market_pairs")]
        public int NumMarketPairs { get; set; }

        [JsonProperty(PropertyName = "date_added")]
        public DateTime? DateAdded { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<object>? Tags { get; set; }

        [JsonProperty(PropertyName = "max_supply")]
        public double? MaxSupply { get; set; }

        [JsonProperty(PropertyName = "circulating_supply")]
        public double? CirculatingSupply { get; set; }

        [JsonProperty(PropertyName = "total_supply")]
        public double? TotalSupply { get; set; }

        [JsonProperty(PropertyName = "platform")]
        public CurrencyInfo Platform { get; set; } = new CurrencyInfo();

        [JsonProperty(PropertyName = "cmc_rank")]
        public int CmcRank { get; set; }

        [JsonProperty(PropertyName = "last_updated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty(PropertyName = "quote")]
        public Quote Quote { get; set; } = new Quote();
    }
}
