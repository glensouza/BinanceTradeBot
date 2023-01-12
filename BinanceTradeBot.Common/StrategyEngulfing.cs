namespace BinanceTradeBot.Common;

// ​The Engulfing Algorithm: This algorithms waits for a specific type of engulfing candles before it enters long or short
public class StrategyEngulfing : IStrategies
{
    public StrategyEngulfing()
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

    ~StrategyEngulfing()
    {
        this.ReleaseUnmanagedResources();
    }
}
