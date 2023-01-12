namespace BinanceTradeBot.Common;

public interface IStrategies : IDisposable
{
    public Task RunStrategyAsync();
}
