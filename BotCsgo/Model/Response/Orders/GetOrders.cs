using BotCsgo.Controller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response
{
    public class GetOrders
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        public orders[] Orders { get; set; }
    }
    public class orders
    {
        [JsonProperty("i_classid")]
        public string I_classid { get; set; }
        [JsonProperty("i_instanceid")]
        public string I_instanceid { get; set; }
        [JsonProperty("i_market_hash_name")]
        public string I_market_hash_name { get; set; }
        [JsonProperty("i_market_name")]
        public string I_market_name { get; set; }
        [JsonProperty("o_price")]
        public string O_price { get; set; }
        [JsonProperty("o_state")]
        public string O_state { get; set; }
        public string Info { get; set; }
        public int Count { get; set; } = 5;
    }
}
