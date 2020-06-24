using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response
{
    class remove_all_from_sale
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
