namespace BinanceTradeBot.Common;

// ​Nadarya Watson Algorithm: This bot is based on the viral Nadarya Watson indicator from trading view.
public class StrategyNadaryaWatson : IStrategies
{
    public StrategyNadaryaWatson()
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

    ~StrategyNadaryaWatson()
    {
        this.ReleaseUnmanagedResources();
    }
}
