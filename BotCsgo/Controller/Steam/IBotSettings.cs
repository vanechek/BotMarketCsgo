using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Controller.Steam
{
    interface IBotSettings
    {
        string url { get; set; }
        string Login { get; set; }
        string Pass { get; set; }
    }
}
