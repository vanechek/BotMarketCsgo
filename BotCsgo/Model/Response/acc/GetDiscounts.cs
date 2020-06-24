using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Model.Response.acc
{
    public class GetDiscounts
    {
        public bool success { get; set; }
        public discounts discounts { get; set; }
    }
    public class discounts
    {
        public double total_buy { get; set; }
        public string buy_discount { get; set; }
        public double total_sell { get; set; }
        public string sell_fee { get; set; }
    }
}
