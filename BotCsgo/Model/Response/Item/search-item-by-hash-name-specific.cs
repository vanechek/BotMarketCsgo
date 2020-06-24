using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response
{
    class search_item_by_hash_name_specific
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        public data[] data { get; set; }
    }

    public class data
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("market_hash_name")]
        public string Market_hash_name { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }
        [JsonProperty("class")]
        public long Class { get; set; }
        [JsonProperty("instance")]
        public int Instance { get; set; }
        [JsonProperty("extra")]
        public extra Extra { get; set; }
    }

    public class extra
    {
        [JsonProperty("extra")]
        public float Float { get; set; } 
        [JsonProperty("phase")]
        public string Phase { get; set; } 
    }
}
