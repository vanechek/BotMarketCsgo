using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response
{
    class buy_items
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("time")]
        public long Time { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("items")]
        public items[] Items { get; set; }

    }
}
