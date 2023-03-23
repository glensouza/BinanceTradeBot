using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BinanceTradeBot.CoinMarketCap
{
    public class ItemData
    {
        private Dictionary<string, Item> dataItemList = new Dictionary<string, Item>();

        [JsonProperty(PropertyName = "status")]
        public Status Status { get; set; } = new Status();

        [JsonProperty(PropertyName = "data")]
        public List<Item> DataList { get; set; } = new List<Item>();

        [JsonProperty(PropertyName = "dataItem")]
        public Dictionary<string, Item> DataItemList
        {
            get => this.dataItemList;
            set
            {
                this.dataItemList = value;
                this.DataList = value.Values.ToList();
            }
        }
    }
}
