using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response
{
    class ListItems
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("items")]
        public items[] Items { get; set; }
    }

}
