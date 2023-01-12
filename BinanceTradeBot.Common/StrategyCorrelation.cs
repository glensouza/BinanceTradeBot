namespace BinanceTradeBot.Common;

// The Correlation Algorithm: This algorithm looks at the correlation between a bunch of asserts like BTC & ETH then waits until it sees divergence and then enter a long or short position
public class StrategyCorrelation : IStrategies
{
    public StrategyCorrelation()
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

    ~StrategyCorrelation()
    {
        this.ReleaseUnmanagedResources();
    }
}
