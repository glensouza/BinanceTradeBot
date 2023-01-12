namespace BinanceTradeBot.Common;

// ​The Breakout Algo: This is a trading algorithm that is built to capture breakouts or breakdowns 
public class StrategyBreakout : IStrategies
{
    public StrategyBreakout()
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

    ~StrategyBreakout()
    {
        this.ReleaseUnmanagedResources();
    }
}
