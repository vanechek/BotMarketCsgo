using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response
{
    class GetCounters
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("items_on_sale")]
        public int Items_on_sale { get; set; }
        [JsonProperty("orders")]
        public int Orders { get; set; }
        [JsonProperty("notify")]
        public int Notify { get; set; }
    }
}
