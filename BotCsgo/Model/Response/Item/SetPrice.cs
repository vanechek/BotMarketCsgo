using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response.Item
{
    class SetPrice
    {
        public string result { get; set; }
        public long item_id { get; set; }
        public int price { get; set; }
        public string price_text { get; set; }
        public string status { get; set; }
        public int position { get; set; }
        public bool success { get; set; }
    }
}
