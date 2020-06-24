using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BotCsgo.Model.Response.Orders
{
    class ProcessOrder
    {
        public bool success { get; set; }
        public string way { get; set; }
        public int price { get; set; }

    }
}
