using System;
using System.Threading.Tasks;

namespace BinanceTradeBot.Common
{
    // Order Book Stalking Algo: It works for anytime frame, stop loss or take profit. it’s super easy to modify and very well documented
    public class StrategyOrderBookStalking : IStrategies
    {
        public StrategyOrderBookStalking()
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

        ~StrategyOrderBookStalking()
        {
            this.ReleaseUnmanagedResources();
        }
    }
}