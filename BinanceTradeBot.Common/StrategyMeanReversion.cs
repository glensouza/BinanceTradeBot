using System;
using System.Threading.Tasks;

namespace BinanceTradeBot.Common
{
    // Mean Reversion Algo trades a mean reversion strategy
    public class StrategyMeanReversion : IStrategies
    {
        public StrategyMeanReversion()
        {
        }

        public Task RunStrategyAsync()
        {
            throw new NotImplementedException();

            // Better Close
            // % change in price away from sma
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

        ~StrategyMeanReversion()
        {
            this.ReleaseUnmanagedResources();
        }
    }
}