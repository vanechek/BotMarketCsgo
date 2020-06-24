using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response.Orders
{
    class DeleteOrders
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("deleted_orders")]
        public int Deleted_orders { get; set; }
    }
}
