using System;
using Newtonsoft.Json;

namespace BinanceTradeBot.CoinMarketCap
{
    public class Status
    {
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty(PropertyName = "error_code")]
        public int ErrorCode { get; set; }

        [JsonProperty(PropertyName = "error_message")]
        public object? ErrorMessage { get; set; }

        [JsonProperty(PropertyName = "elapsed")]
        public int Elapsed { get; set; }

        [JsonProperty(PropertyName = "credit_count")]
        public int CreditCount { get; set; }

        [JsonProperty(PropertyName = "notice")]
        public object? Notice { get; set; }
    }
}
