using System;

namespace BinanceTradeBot.CoinMarketCap
{
    public class Currency
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Volume24hUsd { get; set; }
        public double MarketCapUsd { get; set; }
        public double PercentChange1h { get; set; }
        public double PercentChange24h { get; set; }
        public double PercentChange7d { get; set; }
        public DateTime LastUpdated { get; set; }
        public Double MarketCapConvert { get; set; }
        public string ConvertCurrency { get; set; } = string.Empty;
    }
}
