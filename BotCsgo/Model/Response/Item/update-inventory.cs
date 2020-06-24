using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response
{
    class update_inventory
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
