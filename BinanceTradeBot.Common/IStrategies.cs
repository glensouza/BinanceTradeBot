using System;
using System.Threading.Tasks;

namespace BinanceTradeBot.Common
{
    public interface IStrategies : IDisposable
    {
        Task RunStrategyAsync();
    }
}
