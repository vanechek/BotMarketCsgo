using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response.Item.trade
{
    class trade_request_give
    {
        public bool success { get; set; }
        public string trade { get; set; }
        public string nick { get; set; }
        public string botid { get; set; }
        public string profile { get; set; }
        public string secret { get; set; }
        public List<long> items { get; set; }
    }
}
