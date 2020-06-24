using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response.Orders
{
    class StatusOrders
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("online")]
        public bool Online { get; set; }
        [JsonProperty("has_items")]
        public bool has_items { get; set; }
    }
}
