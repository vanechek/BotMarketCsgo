using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response
{
    class add_to_sale
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("item_id")]
        public long Item_id { get; set; }
    }
}
