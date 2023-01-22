using System;
using System.Threading.Tasks;

namespace BinanceTradeBot.Common
{
    //Turtle Trending Algorithm: This algorithm analyzes the past 55 time periods and buys and sells based off the overall market trend + a few indicator
    public class StrategyTurtleTrending : IStrategies
    {
        public StrategyTurtleTrending()
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

        ~StrategyTurtleTrending()
        {
            this.ReleaseUnmanagedResources();
        }
    }
}