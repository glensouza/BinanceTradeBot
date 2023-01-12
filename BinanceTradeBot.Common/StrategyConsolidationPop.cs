namespace BinanceTradeBot.Common;

// Consolidation Pop Algo:  This is an amazing “counter trading” algorithm because as many traders believe the right move to do is to short near resistance, this algo takes advantage of that
public class StrategyConsolidationPop : IStrategies
{
    public StrategyConsolidationPop()
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

    ~StrategyConsolidationPop()
    {
        this.ReleaseUnmanagedResources();
    }
}
