using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Controller.Steam
{
    class BotSteamSetting : IBotSettings
    {
        public BotSteamSetting(string login, string pass)
        {
            Login = login;
            Pass = pass;
        }

        public string url { get; set; } = "https://store.steampowered.com";
        public string Login { get; set; }
        public string Pass { get; set; }
    }
}
