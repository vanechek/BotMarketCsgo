using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response.Orders
{
    public class Log
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("classid")]
        public long Classid { get; set; }
        [JsonProperty("instanceid")]
        public int Instanceid { get; set; }
        [JsonProperty("name")]
        public string Market_hash_name { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("quality")]
        public string Quality { get; set; }
        [JsonProperty("rarity")]
        public string Rarity { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
        [JsonProperty("executed")]
        public long Executed { get; set; }
    }

    public class GetOrdersLog
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("log")]
        public Log[] Log { get; set; }
    }
}
