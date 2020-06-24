using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response
{
    class get_money
    {
        [JsonProperty("money")]
        public decimal Money { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
