using System;

namespace BotCsgo.Model
{
    [Serializable]
    public class BotConfiguration
    {
        public string SteamLogin;
        public string SteamPassword;
        public string AutoOrderMax;
        public bool AutoOrders;
        public bool AutoPriceInventory;
        public bool AutoAcceptTrade;
        public string FreezeBox;

        public BotConfiguration() { }

        public BotConfiguration(string steamLogin, string steamPassword, string autoOrderMax, bool autoOrders, bool autoPriceInventory ,bool autoAcceptTrade, string freezeBox)
        {
            SteamLogin = steamLogin;
            SteamPassword = steamPassword;
            AutoOrderMax = autoOrderMax;
            AutoOrders = autoOrders;
            AutoPriceInventory = autoPriceInventory;
            AutoAcceptTrade = autoAcceptTrade;
            FreezeBox = freezeBox;
        }
    }
}
