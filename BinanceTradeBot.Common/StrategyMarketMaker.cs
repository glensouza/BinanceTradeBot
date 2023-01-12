namespace BinanceTradeBot.Common;

// The Market Maker Algo is meant to work in any market and trade long and short
public class StrategyMarketMaker : IStrategies
{
    public StrategyMarketMaker()
    {
    }

    public async Task RunStrategyAsync()
    {
        throw new NotImplementedException();
    }

    private void ReleaseUnmanagedResources()
    {
        // TODO: release unmanaged resources here
    }

    public void Dispose()
    {
        this.ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~StrategyMarketMaker()
    {
        this.ReleaseUnmanagedResources();
    }
}
