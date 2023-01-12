using Binance.API.Csharp.Client;
using Binance.API.Csharp.Client.Domain.Interfaces;
using Binance.API.Csharp.Client.Models.Enums;
using Binance.API.Csharp.Client.Models.Market;

namespace BinanceTradeBot.Common;

public class StrategyMovingAverage : IStrategies
{
    private readonly decimal currentPrice;
    private readonly decimal maShortTerm;
    private readonly decimal maLongTerm;
    private readonly string symbol;
    private readonly bool testMode;
    private BinanceClient? binanceClient;
    private DateTime buyTime;
    private MovingAverageStrategies strategy;

    public StrategyMovingAverage(
        string symbol = "BTCUSDT",
        MovingAverageStrategies strategy = MovingAverageStrategies.SimpleMovingAverage,
        int shortTermMA = 20,
        int longTermMA = 40,
        string binanceApiKey = "X7vcxwDLcpZtHLoQbr8KPdly4K8bhbxkUSgT0TusKYC3lPvVsZGyL4BIJZB1lXww",
        string binanceApiSecret = "MCdx2ZvuPR6pppVocj9q9QczxpetyxdMRyAWHzZDswtnaU8mpX7ky0oJUn511dBU",
        bool testMode = true)
    {
        this.testMode = testMode;
        this.strategy = strategy;
        this.symbol = symbol;

        //Initialize the Binance API client
        ApiClient apiClient = new(binanceApiKey, binanceApiSecret);
        this.binanceClient = new BinanceClient(apiClient);
        
        //Retrieve historical data
        IEnumerable<Candlestick>? historicalData = this.binanceClient.GetCandleSticks(symbol, TimeInterval.Days_1, limit: longTermMA).Result;
        if (historicalData == null)
        {
            throw new Exception("No historical data found");
        }

        historicalData = historicalData.ToList();
        if (!historicalData.Any())
        {
            throw new Exception("No historical data found");
        }

        //Retrieve the current price of digital currency
        this.currentPrice = historicalData.LastOrDefault()?.Close ?? 0;

        //Calculate the short-term and long-term MAs
        List<decimal> closingPrices = historicalData.Select(x => x.Close).ToList();
        this.maShortTerm = closingPrices.Skip(longTermMA - shortTermMA).Average();
        this.maLongTerm = closingPrices.Average();
    }

    public async Task RunStrategyAsync()
    {
        switch (this.strategy)
        {
            case MovingAverageStrategies.SimpleMovingAverage:
                ////Check if the current price is above the 20-day SMA
                //if (currentPrice > sma20)
                //{
                //    buyTime = DateTime.Now;
                //    //Place a buy order
                //    client.NewOrder("BTCUSDT", currentPrice, OrderSide.Buy, OrderType.Market, TimeInForce.GTC, 1);
                //}

                //if ((DateTime.Now - buyTime).TotalDays > 5)
                //{
                //    //Check if the current price is below the 40-day SMA
                //    if (currentPrice < sma40)
                //    {
                //        //Place a sell order
                //        client.NewOrder("BTCUSDT", currentPrice, OrderSide.Sell, OrderType.Market, TimeInForce.GTC, 1);
                //    }
                //}

                //If the 20-day SMA is greater than the 40-day SMA, and the current price is greater than the 20-day SMA, buy BTC
                if (this.maShortTerm > this.maLongTerm && this.currentPrice > this.maShortTerm)
                {
                    //Buy BTC
                    var order = await this.binanceClient.PostNewOrderTest("BTCUSDT", 0.001m, 0m, OrderSide.BUY, OrderType.MARKET);
                    //If the order was successful, set the buy time
                    //if (order.Success)
                    //{
                    //    buyTime = DateTime.Now;
                    //}
                }
                //If the 20-day SMA is less than the 40-day SMA, and the current price is less than the 20-day SMA, sell BTC
                else if (this.maShortTerm < this.maLongTerm && this.currentPrice < this.maShortTerm)
                {
                    //Sell BTC
                    //var order = client.PostNewOrder("BTCUSDT" , 0.001m, 0m, OrderSide.SELL, OrderType.MARKET);
                    //If the order was successful, set the buy time to null
                    //if (order.Success)
                    //{
                    //    buyTime = null;
                    //}
                }
                //If the current price is greater than the 20-day SMA, and the buy time is greater than 24 hours, sell BTC
                else if (this.currentPrice > this.maShortTerm && this.buyTime != null && DateTime.Now - this.buyTime > TimeSpan.FromHours(24))
                {
                    //Sell BTC
                    //var order = client.PostNewOrder("BTCUSDT", 0.001m, 0m, OrderSide.SELL, OrderType.MARKET);
                    //If the order was successful, set the buy time to null
                    //if (order.Success)
                    //{
                    //    buyTime = null;
                    //}
                }
                break;
            case MovingAverageStrategies.ExponentialMovingAverage:
                /*
                 *EMA
                 * The EMA is calculated using a formula that gives more weight to recent prices, making it more responsive to recent price changes. The formula used in this example is :
                 * EMA = (currentPrice - previousEMA) * (2 / (timePeriod + 1)) + previousEMA
                 * You would also need to keep track of previous EMA values to calculate the next one and update them in each iteration.
                public void CheckPrice() {
                        currentPrice = client.GetPrice("BTCUSDT").Price;
                        ema20 = (currentPrice - previousEma20) * (2 / 21) + previousEma20;
                        ema40 = (currentPrice - previousEma40) * (2 / 41) + previousEma40;
                        previousEma20 = ema20;
                        previousEma40 = ema40;
                        //Check if the current price is above the 20-day EMA
                        if (currentPrice > ema20) {
                            buyTime = DateTime.Now;
                            //Place a buy order
                            client.NewOrder("BTCUSDT", currentPrice, OrderSide.Buy, OrderType.Market, TimeInForce.GTC, 1);
                        }
                    }

                    public void SellCheck() {
                        if((DateTime.Now - buyTime).TotalDays > 5)
                        {
                            //Check if the current price is below the 40-day EMA
                            if (currentPrice < ema40) {
                                //Place a sell order
                                client.NewOrder("BTCUSDT", currentPrice, OrderSide.Sell, OrderType.Market, TimeInForce.GTC, 1);
                            }
                        }
                    }
                */
                break;
            case MovingAverageStrategies.WeightedMovingAverage:
                /*
                 *WMA
                 * The WMA is calculated by giving more weight to the more recent prices and less weight to the older prices. The formula used in this example is:
                 * WMA = (sum of (price[i] * weight[i])) / (sum of weights)
                 * where weight[i] = (period - i)
                 * I have also added two arrays to keep track of the previous prices for each period, and a helper function CalculateWMA to calculate the WMA based on the array of prices and the period.
                 * You would need to update these arrays with the current price in each iteration and then recalculate the WMA values.
                using System;
                using System.Linq;
                using Binance.API.Csharp.Client;
                using Binance.API.Csharp.Client.Models.Enums;

                class Strategy {
                    private BinanceClient client;
                    private decimal currentPrice;
                    private decimal wma20;
                    private decimal wma40;
                    private decimal[] previousPrices20;
                    private decimal[] previousPrices40;
                    private DateTime buyTime;

                    public Strategy() {
                        client = new BinanceClient(); //Initialize the Binance API client
                        //Retrieve historical data for the past 20 days
                        var historicalData = client.GetKlines("BTCUSDT", Interval.OneDay, 20);
                        var closingPrices = historicalData.Data.Select(x => x.Close).ToArray();
                        previousPrices20 = new decimal[20];
                        previousPrices40 = new decimal[40];
                        Array.Copy(closingPrices, previousPrices20, 20);
                        Array.Copy(closingPrices, previousPrices40, 40);
                        //Calculate the WMA values
                        wma20 = CalculateWMA(previousPrices20,20);
                        wma40 = CalculateWMA(previousPrices40,40);
                    }

                    public void CheckPrice() {
                        currentPrice = client.GetPrice("BTCUSDT").Price;
                        //Add current price to the array
                        Array.Copy(previousPrices20, 1, previousPrices20, 0, 19);
                        Array.Copy(previousPrices40, 1, previousPrices40, 0, 39);
                        previousPrices20[19] = currentPrice;
                        previousPrices40[39] = currentPrice;
                        //Recalculate the WMA values
                        wma20 = CalculateWMA(previousPrices20,20);
                        wma40 = CalculateWMA(previousPrices40,40);
                        //Check if the current price is above the 20-day WMA
                        if (currentPrice > wma20) {
                            buyTime = DateTime.Now;
                            //Place a buy order
                            client.NewOrder("BTCUSDT", currentPrice, OrderSide.Buy, OrderType.Market, TimeInForce.GTC, 1);
                        }
                    }

                    public void SellCheck() {
                        if((DateTime.Now - buyTime).TotalDays > 5)
                        {
                            //Check if the current price is below the 40-day WMA
                            if (currentPrice < wma40) {
                                //Place a sell order
                                client.NewOrder("BTCUSDT", currentPrice, OrderSide.Sell, OrderType.Market, TimeInForce.GTC, 1);
                            }
                        }
                    }

                    public decimal CalculateWMA(decimal[] prices, int period) {
                        decimal wma = 0;
                        int j = period;
                        for (int i = 0; i < period; i++) {
                            wma += (prices[i] * (j--));
                        }
                        return wma / (period * (period + 1) / 2);
                    }
                }
                */
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ReleaseUnmanagedResources()
    {
        // release unmanaged resources here
        this.binanceClient = null;
    }

    public void Dispose()
    {
        this.ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~StrategyMovingAverage()
    {
        this.ReleaseUnmanagedResources();
    }
}

// Golden Cross
// Death Cross
/*
 * In this example, I added two additional boolean variables goldenCrossDetected and deathCrossDetected to keep track of whether a "Golden Cross" or "Death Cross" has been detected.
 * The "Golden Cross" occurs when the short-term moving average (in this case the 20-day WMA) crosses above the long-term moving average (in this case the 40-day WMA). This is often considered a bullish signal and a buy signal.
 * The "Death Cross" occurs when the short-term moving average (in this case the 20-day WMA) crosses below the long-term moving average (in this case the 40-day WMA). This is often considered a bearish signal and a sell signal.
 * I added a check in the CheckPrice method to check if the 20-day WMA is greater than the 40-day WMA, and if it is and a Golden cross has not been detected before, the method will set goldenCrossDetected to true, deathCrossDetected to false and print a message "Golden Cross detected!" on the console.
 * Similarly, I added another check in the CheckPrice method to check if the 20-day WMA is less than the 40-day WMA, and if it is and a Death cross has not been detected before, the method will set deathCrossDetected to true, goldenCrossDetected to false and print a message "Death Cross detected!" on the console.

using System;
using System.Linq;
using Binance.API.Csharp.Client;
using Binance.API.Csharp.Client.Models.Enums;

class Strategy {
    private BinanceClient client;
    private decimal currentPrice;
    private decimal wma20;
    private decimal wma40;
    private decimal[] previousPrices20;
    private decimal[] previousPrices40;
    private DateTime buyTime;
    private bool goldenCrossDetected = false;
    private bool deathCrossDetected = false;

    public Strategy() {
        client = new BinanceClient(); //Initialize the Binance API client
        //Retrieve historical data for the past 20 days
        var historicalData = client.GetKlines("BTCUSDT", Interval.OneDay, 20);
        var closingPrices = historicalData.Data.Select(x => x.Close).ToArray();
        previousPrices20 = new decimal[20];
        previousPrices40 = new decimal[40];
        Array.Copy(closingPrices, previousPrices20, 20);
        Array.Copy(closingPrices, previousPrices40, 40);
        //Calculate the WMA values
        wma20 = CalculateWMA(previousPrices20,20);
        wma40 = CalculateWMA(previousPrices40,40);
    }

    public void CheckPrice() {
        currentPrice = client.GetPrice("BTCUSDT").Price;
        //Add current price to the array
        Array.Copy(previousPrices20, 1, previousPrices20, 0, 19);
        Array.Copy(previousPrices40, 1, previousPrices40, 0, 39);
        previousPrices20[19] = currentPrice;
        previousPrices40[39] = currentPrice;
        //Recalculate the WMA values
        wma20 = CalculateWMA(previousPrices20,20);
        wma40 = CalculateWMA(previousPrices40,40);

        //Check for Golden Cross
        if (!goldenCrossDetected && wma20 > wma40) {
            goldenCrossDetected = true;
            deathCrossDetected = false;
            Console.WriteLine("Golden Cross detected!");
        }

        //Check for Death Cross
        if (!deathCrossDetected && wma20 < wma40) {
            deathCrossDetected = true;
            goldenCrossDetected = false;
            Console.WriteLine("Death Cross detected!");
        }

        //Check if the current price is above the 20-day WMA
        if (currentPrice > wma20) {
            buyTime = DateTime.Now;
            //Place a buy order
            client.NewOrder("BTCUSDT", currentPrice, OrderSide.Buy, OrderType.Market, TimeInForce.GTC, 1);
        }
    }

    public void SellCheck() {
        if((DateTime.Now - buyTime).TotalDays > 5)
        {
            //Check if the current price is below the 40-day WMA
            if (currentPrice < wma40) {
                //Place a sell order
                client.NewOrder("BTCUSDT", currentPrice, OrderSide.Sell, OrderType.Market, TimeInForce.GTC, 1);
            }
        }
    }

    public decimal CalculateWMA(decimal[] prices, int period) {
        decimal wma = 0;
        int j = period;
        for (int i = 0; i < period; i++) {
            wma += (prices[i] * (j--));
        }
        return wma / (period * (period + 1) / 2);
    }
}
*/

// STOP LOSS
/*
 * In this code, I added a new method called StopLossCheck which will check if the current price is less than the stop-loss price. If it is, it will place a sell order and print a message "Stop loss triggered. Selling at {currentPrice}" on the console.
 * Also, I added a new variable stopLoss and buyPrice which will store the stop loss price and buy price respectively. I set the stop loss to be 5% below the buy price in the CheckPrice method.

using System;
using System.Linq;
using Binance.API.Csharp.Client;
using Binance.API.Csharp.Client.Models.Enums;

class Strategy {
    private BinanceClient client;
    private decimal currentPrice;
    private decimal wma20;
    private decimal wma40;
    private decimal[] previousPrices20;
    private decimal[] previousPrices40;
    private DateTime buyTime;
    private decimal buyPrice;
    private decimal stopLoss;
    private bool goldenCrossDetected = false;
    private bool deathCrossDetected = false;

    public Strategy() {
        client = new BinanceClient(); //Initialize the Binance API client
        //Retrieve historical data for the past 20 days
        var historicalData = client.GetKlines("BTCUSDT", Interval.OneDay, 20);
        var closingPrices = historicalData.Data.Select(x => x.Close).ToArray();
        previousPrices20 = new decimal[20];
        previousPrices40 = new decimal[40];
        Array.Copy(closingPrices, previousPrices20, 20);
        Array.Copy(closingPrices, previousPrices40, 40);
        //Calculate the WMA values
        wma20 = CalculateWMA(previousPrices20,20);
        wma40 = CalculateWMA(previousPrices40,40);
    }

    public void CheckPrice() {
        currentPrice = client.GetPrice("BTCUSDT").Price;
        //Add current price to the array
        Array.Copy(previousPrices20, 1, previousPrices20, 0, 19);
        Array.Copy(previousPrices40, 1, previousPrices40, 0, 39);
        previousPrices20[19] = currentPrice;
        previousPrices40[39] = currentPrice;
        //Recalculate the WMA values
        wma20 = CalculateWMA(previousPrices20,20);
        wma40 = CalculateWMA(previousPrices40,40);

        //Check for Golden Cross
        if (!goldenCrossDetected && wma20 > wma40) {
            goldenCrossDetected = true;
            deathCrossDetected = false;
            Console.WriteLine("Golden Cross detected!");
        }

        //Check for Death Cross
        if (!deathCrossDetected && wma20 < wma40) {
            deathCrossDetected = true;
            goldenCrossDetected = false;
            Console.WriteLine("Death Cross detected!");
        }

        //Check if the current price is above the 20-day WMA
        if (currentPrice > wma20) {
            buyTime = DateTime.Now;
            buyPrice = currentPrice;
            stopLoss = buyPrice * 0.95m; //stop loss at 5% below the buy price
            //Place a buy order
            client.NewOrder("BTCUSDT", currentPrice, OrderSide.Buy, OrderType.Market, TimeInForce.GTC, 1);
        }
    }
    //Check for stop loss
    public void StopLossCheck() {
        if (currentPrice < stopLoss) {
            //Place a sell order
            client.NewOrder("BTCUSDT", currentPrice, OrderSide.Sell, OrderType.Market, TimeInForce.GTC, 1);
            Console.WriteLine("Stop loss triggered. Selling at {0}", currentPrice);
        }
    }

    public void SellCheck() {
        if((DateTime.Now - buyTime).TotalDays > 5)
        {
            //Check if the current price is below the 40-day WMA
            if (currentPrice < wma40) {
                //Place a sell order
                client.NewOrder("BTCUSDT", currentPrice, OrderSide.Sell, OrderType.Market, TimeInForce.GTC, 1);
            }
        }
    }

    public decimal CalculateWMA(decimal[] prices, int period) {
        decimal wma = 0;
        int j = period;
        for (int i = 0; i < period; i++) {
            wma += (prices[i] * (j--));
        }
        return wma / (period * (period + 1) / 2);
    }
}
*/

// Check if we're already in a position
/*
 * In this code, I added a new variable called inPosition which will store whether we're in a position or not. I set it to true when we place a buy order and set it to false when we place a sell order. I also added a check in the CheckPrice method to make sure we're not already in a position before placing a buy order.
 * In this example, I added a new variable inPosition which is a boolean variable that will keep track of whether or not the strategy is currently in a position. Initially, it is set to false.
 * In the CheckPrice method, before placing a buy order I added a check to see if we are not in a position and if it is true, only then it will place a buy order and set the inPosition variable to true.
 * In the SellCheck method, after selling, I set the inPosition variable to false.

using System;
using System.Linq;
using Binance.API.Csharp.Client;
using Binance.API.Csharp.Client.Models.Enums;

class Strategy {
    private BinanceClient client;
    private decimal currentPrice;
    private decimal wma20;
    private decimal wma40;
    private decimal[] previousPrices20;
    private decimal[] previousPrices40;
    private DateTime buyTime;
    private decimal buyPrice;
    private decimal stopLoss;
    private bool goldenCrossDetected = false;
    private bool deathCrossDetected = false;
    private bool inPosition = false;

    public Strategy() {
        client = new BinanceClient(); //Initialize the Binance API client
        //Retrieve historical data for the past 20 days
        var historicalData = client.GetKlines("BTCUSDT", Interval.OneDay, 20);
        var closingPrices = historicalData.Data.Select(x => x.Close).ToArray();
        previousPrices20 = new decimal[20];
        previousPrices40 = new decimal[40];
        Array.Copy(closingPrices, previousPrices20, 20);
        Array.Copy(closingPrices, previousPrices40, 40);
        //Calculate the WMA values
        wma20 = CalculateWMA(previousPrices20,20);
        wma40 = CalculateWMA(previousPrices40,40);
    }

    public void CheckPrice() {
        currentPrice = client.GetPrice("BTCUSDT").Price;
        //Add current price to the array
        Array.Copy(previousPrices20, 1, previousPrices20, 0, 19);
        Array.Copy(previousPrices40, 1, previousPrices40, 0, 39);
        previousPrices20[19] = currentPrice;
        previousPrices40[39] = currentPrice;
        //Recalculate the WMA values
        wma20 = CalculateWMA(previousPrices20,20);
        wma40 = CalculateWMA(previousPrices40,40);

        //Check for Golden Cross
        if (!goldenCrossDetected && wma20 > wma40) {
            goldenCrossDetected = true;
            deathCrossDetected = false;
            Console.WriteLine("Golden Cross detected!");
        }

        //Check for Death Cross
        if (!deathCrossDetected && wma20 < wma40) {
            deathCrossDetected = true;
            goldenCrossDetected = false;
            Console.WriteLine("Death Cross detected!");
        }

        //Check if the current price is above the 20-day WMA and we're not in a position
        if (currentPrice > wma20 && !inPosition) {
            buyTime = DateTime.Now;
            buyPrice = currentPrice;
            stopLoss = buyPrice * 0.95m; //stop loss at 5% below the buy price
            inPosition = true;
            //Place a buy order
        client.NewOrder("BTCUSDT", currentPrice, OrderSide.Buy, OrderType.Market, TimeInForce.GTC, 1);
        Console.WriteLine("Bought at {0}", currentPrice);
        }
    }

    public void SellCheck() {
        if((DateTime.Now - buyTime).TotalDays > 5)
        {
            //Check if the current price is below the 40-day WMA
            if (currentPrice < wma40) {
                inPosition = false;
                //Place a sell order
                client.NewOrder("BTCUSDT", currentPrice, OrderSide.Sell, OrderType.Market, TimeInForce.GTC, 1);
                Console.WriteLine("Sold at {0}", currentPrice);
            }
        }
    }

    public decimal CalculateWMA(decimal[] prices, int period) {
        decimal wma = 0;
        int j = period;
        for (int i = 0; i < period; i++) {
            wma += (prices[i] * (j--));
        }
        return wma / (period * (period + 1) / 2);
    }
}
*/