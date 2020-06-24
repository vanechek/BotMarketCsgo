using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response
{
    class marketping
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("ping")]
        public string Ping { get; set; }
    }
}
