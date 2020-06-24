using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response.Item
{
    class ItemRequest
    {
        public bool success { get; set; }
        public string trade { get; set; }
        public string nick { get; set; }
        public string botid { get; set; }
        public string profile { get; set; }
        public string secret { get; set; }
        public string[] items { get; set; }
    }
}
