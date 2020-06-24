using BotCsgo.Controller;
using BotCsgo.Controller.Steam;
using BotCsgo.Model;
using BotCsgo.Model.Response;
using BotCsgo.Model.Response.Orders;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace BotCsgo.ViewModel
{
    public class BotPageModel : BaseViewModel
    {
        #region Binding
        private string _Url;
        public string Url
        {
            get { return _Url; }
            set { _Url = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Log> _ListOrdersLog;
        public ObservableCollection<Log> ListOrdersLog
        {
            get { return _ListOrdersLog; }
            set { _ListOrdersLog = value; OnPropertyChanged(); }
        }
        private Visibility _StartButtonVisiability;
        public Visibility StartButtonVisiability
        {
            get => _StartButtonVisiability;
            set { _StartButtonVisiability = value; OnPropertyChanged(); }
        }
        private Visibility _StopButtonVisiability;
        public Visibility StopButtonVisiability
        {
            get => _StopButtonVisiability;
            set { _StopButtonVisiability = value; OnPropertyChanged(); }
        }
        private string _LoginBox;
        public string LoginBox
        {
            get { return _LoginBox; }
            set { _LoginBox = value; OnPropertyChanged(); }
        }
        public string _PassBox;
        public string PassBox
        {
            get { return _PassBox; }
            set { _PassBox = value; OnPropertyChanged(); }
        }
        private bool _Checked;
        public bool Checked
        {
            get { return _Checked; }
            set { _Checked = value; OnPropertyChanged(); }
        }
        private string _TokenTrade;
        public string TokenTrade
        {
            get { return _TokenTrade; }
            set { _TokenTrade = value; OnPropertyChanged(); }
        }
        private bool _AutoAcceptTrade;
        public bool AutoAcceptTrade
        {
            get { return _AutoAcceptTrade; }
            set { _AutoAcceptTrade = value; OnPropertyChanged(); }
        }
        private bool _AutoOrders = false;
        public bool AutoOrders
        {
            get { return _AutoOrders; }
            set { _AutoOrders = value; OnPropertyChanged(); }
        }
        private bool _AutoPriceInventory = false;
        public bool AutoPriceInventory
        {
            get { return _AutoPriceInventory; }
            set { _AutoPriceInventory = value; OnPropertyChanged(); }
        }
        private bool _IsEnableBrowser;
        public bool IsEnableBrowser
        {
            get { return _IsEnableBrowser; }
            set { _IsEnableBrowser = value; OnPropertyChanged(); }
        }
        private string _AutoOrderMax;
        public string AutoOrderMax
        {
            get { return _AutoOrderMax; }
            set { _AutoOrderMax = value; OnPropertyChanged(); }
        }
        private string _TextLog;
        public string TextLog
        {
            get { return _TextLog; }
            set { _TextLog = value; OnPropertyChanged(); }
        }
        private string _FreezeBox = "30";
        public string FreezeBox
        {
            get { return _FreezeBox; }
            set { _FreezeBox = value; OnPropertyChanged(); }
        }
        private ObservableCollection<orders> _ListOrders;
        public ObservableCollection<orders> ListOrders
        {
            get { return _ListOrders; }
            set { _ListOrders = value; OnPropertyChanged(); }
        }
        #endregion
        #region Commands
        public ICommand Start { get; }
        public ICommand Stop { get; }
        public ICommand SaveConfiguration { get; }
        #endregion
        private MarketApi api;
        private BotSteamSetting steamSetting;
        private SteamBot SteamBot;
        private bool StopBot { get; set; } = true;
        private items CurrentItemReceived { get; set; }
        public BotPageModel()
        {
            StopButtonVisiability = Visibility.Collapsed;
            api = new MarketApi(KeyContorller.LoadKey());
            Start = new RelayCommand((obj) => StartAsyncs(AutoAcceptTrade, AutoOrders, AutoPriceInventory));
            Stop = new RelayCommand((obj) => StopBotClicked());
            SaveConfiguration = new RelayCommand((obj) => Save());
            GetListOrders();
            var cfg = LoadCfg();
            if (cfg != null)
            {
                LoginBox = cfg.SteamLogin;
                PassBox = cfg.SteamPassword;
                AutoPriceInventory = cfg.AutoPriceInventory;
                AutoOrders = cfg.AutoOrders;
                AutoPriceInventory = cfg.AutoPriceInventory;
                AutoAcceptTrade = cfg.AutoAcceptTrade;
                FreezeBox = cfg.FreezeBox;
                AutoOrderMax = cfg.AutoOrderMax;
            }
            StopButtonClicked += () =>
             {
                 StopBot = false;
             };
        }
        public async void GetListOrders()
        {
            bool fistStart = false;
            int countOrders = 0;
            int countOrdersLog = 0;
            while (true)
            {
                await Task.Run(async () =>
                {
                    try
                    {
                        var orders = await api.GetOrders();
                        var logs = await api.GetOrdersLog();
                        if (orders != null && logs != null)
                        {
                            if (!fistStart)
                            {
                                ListOrders = orders;
                                ListOrdersLog = logs;
                                countOrders = ListOrders.Count;
                                countOrdersLog = ListOrdersLog.Count;
                                fistStart = true;
                            }
                            if (logs.Count != countOrdersLog)
                            {
                                for (int i = 0; i < logs.Count; i++)
                                {
                                    var orderLogIsNew = CheckNewOrderLog(logs[i]);
                                    if (orderLogIsNew == false)
                                    {
                                        ListOrdersLog.Add(logs[i]);
                                    }
                                }
                            }
                            if (orders.Count != countOrders)
                            {
                                for (int i = 0; i < orders.Count; i++)
                                {
                                    var orderIsNew = CheckNewOrder(orders[i]);
                                    if (orderIsNew == false)
                                    {
                                        ListOrders.Add(orders[i]);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        await Task.Delay(250);
                    }
                });
            }
        }
        public bool CheckNewOrderLog(Log log)
        {
            for (int i = 0; i < ListOrdersLog.Count; i++)
            {
                if (ListOrdersLog[i] == log)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckNewOrder(orders orders)
        {
            for (int i = 0; i < ListOrders.Count; i++)
            {
                if (ListOrders[i] == orders)
                {
                    return true;
                }
            }
            return false;
        }
        public event Action StopButtonClicked;
        public BotConfiguration LoadCfg()
        {
            BotConfiguration botConfiguration = new BotConfiguration(LoginBox, PassBox, AutoOrderMax, AutoOrders, AutoPriceInventory, AutoAcceptTrade, FreezeBox);
            XmlSerializer formatter = new XmlSerializer(typeof(BotConfiguration));
            try
            {
                using (FileStream fs = new FileStream("cfg.xml", FileMode.Open))
                {
                    botConfiguration = formatter.Deserialize(fs) as BotConfiguration;
                }
            }
            catch (Exception)
            {
                Save();
            }
            return botConfiguration;
        }
        public void Save()
        {
            BotConfiguration botConfiguration = new BotConfiguration(LoginBox, PassBox, AutoOrderMax, AutoOrders, AutoPriceInventory, AutoAcceptTrade, FreezeBox);
            XmlSerializer formatter = new XmlSerializer(typeof(BotConfiguration));
            using (FileStream fs = new FileStream("cfg.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, botConfiguration);
            }
        }
        public void StopBotClicked()
        {
            StopButtonClicked?.Invoke();
        }

        public async void StartAsyncs(bool autoAcceptTrade, bool autoOrders, bool autoPriceInventory)
        {
            StartButtonVisiability = Visibility.Collapsed;
            StopButtonVisiability = Visibility.Visible;
            bool BotStart = false;
            bool ItemReceived = false;
            bool ItemBuyed = false;
            items CurrentItemReceived = null;
            items CurrentItemBuyed = null;
            while (StopBot)
            {
                await Task.Run(async () =>
                {
                    try
                    {
                        if (autoAcceptTrade)
                        {
                            if (BotStart == false)
                            {
                                steamSetting = new BotSteamSetting(LoginBox, PassBox);
                                SteamBot = new SteamBot(steamSetting);
                                SteamBot.Start();
                                BotStart = true;
                            }
                            var items = await api.GetOrdersWithStatus();
                            for (int i = 0; i < items.Count; i++)
                            {
                                switch (items[i].Status)
                                {
                                    case "2":
                                        if (ItemBuyed == false && CurrentItemBuyed != items[i])
                                        {
                                            ItemBuyed = true;
                                            CurrentItemBuyed = items[i];
                                            TextLog += $"{DateTime.Now}: Предмет {items[i].Market_hash_name} продан за {items[i].Price} \n";
                                            await api.ItemReqeustIn(items[i].Botid);
                                        }
                                        else
                                        {
                                            if (CurrentItemBuyed.Classid != items[i].Classid)
                                            {
                                                ItemBuyed = false;
                                                CurrentItemBuyed = null;
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }
                                        continue;
                                    case "3":
                                        AddOrder(items[i]);
                                        continue;
                                    case "4":
                                        if (ItemReceived != true && CurrentItemReceived != items[i])
                                        {
                                            ItemReceived = true;
                                            CurrentItemReceived = items[i];
                                            TextLog += $"{DateTime.Now}: Предмет {items[i].Market_hash_name} куплен за {items[i].Price} \n";
                                            AddOrder(items[i]);
                                            var itemrequest = await api.ItemReqeustOut(items[i].Botid);
                                            if (SteamBot.CheckStatusBot() == true)
                                            {
                                                SteamBot.GetTrade(itemrequest.trade);
                                            }
                                            else
                                            {
                                                SteamBot.GetTrade(itemrequest.trade);
                                                Thread.Sleep(TimeSpan.FromSeconds(5));
                                                SteamBot.Start();
                                            }
                                        }
                                        else
                                        {
                                            if (CurrentItemReceived != items[i])
                                            {
                                                ItemReceived = false;
                                                CurrentItemReceived = null;
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }
                                        continue;
                                    default:
                                        break;
                                }
                            }
                        }
                        if (autoOrders)
                        {
                            int countGet = 0;
                            var balance = await api.GetBalance();
                            string newBalance = string.Empty;
                            if (balance.Equals(",0"))
                            {
                                balance += "0";
                            }
                            if (balance.Contains(","))
                            {
                                newBalance = balance.Replace(",", "");
                            }
                            else
                            {
                                newBalance = balance + "00";
                            }
                            var orders = await api.GetOrders();
                            for (int i = 0; i < orders.Count; i++)
                            {
                                if (orders[i].Info == orders[i].O_price)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (Convert.ToInt32(orders[i].Info) >= Convert.ToInt32(newBalance))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (countGet == 5)
                                        {
                                            countGet = 0;
                                        }
                                        var item = await api.GetItemInfo(orders[i].I_classid, orders[i].I_instanceid);
                                        if (item.Buy_offers != null)
                                        {
                                            var price = Convert.ToInt32(item.Min_price);
                                            var threshold = price * Convert.ToInt32(AutoOrderMax) / 100;
                                            var result = price - threshold;
                                            buy_offers maxBuyOffer = new buy_offers();
                                            foreach (var buy_Offer in item.Buy_offers)
                                            {
                                                maxBuyOffer = buy_Offer;
                                                break;
                                            }
                                            var newPrice = Convert.ToInt32(maxBuyOffer.O_price) + 1;
                                            if (result > newPrice)
                                            {
                                                await api.UpdateOrder(orders[i].I_classid, orders[i].I_instanceid, newPrice.ToString());
                                                countGet += 1;
                                            }
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }
                            if (autoPriceInventory)
                            {
                                try
                                {
                                    var price = await api.GetSellList();
                                    for (int i = 0; i < price.Count; i++)
                                    {
                                        var item = await api.GetFirstPrice(price[i].Market_hash_name);
                                        var currnetPrice = price[i].Price;
                                        if (currnetPrice.Replace(".", "") == currnetPrice)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            if (price[i].Position > 1)
                                            {
                                                api.SetPrice(price[i].Item_id, Convert.ToInt32(currnetPrice) - 1);
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    api.StartPing();
                                }
                            }
                        }
                    }
                    catch
                    {
                        api.StartPing();
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(Convert.ToInt32(FreezeBox)));
                });
            }
            SteamBot.CloseBot();
            StopSellOrders();
            StopButtonVisiability = Visibility.Collapsed;
            StartButtonVisiability = Visibility.Visible;
        }
        public async void AddOrder(items currentItems)
        {
            try
            {
                await Task.Run(async () =>
                {
                    var getItemInfoWithStatusThree = await api.GetItemInfo(currentItems.Classid, currentItems.Instanceid);
                    api.AddOrdersForBot(getItemInfoWithStatusThree.Classid, getItemInfoWithStatusThree.Instanceid);
                });
            }
            catch
            { }
        }
        public static async void StopSellOrders()
        {
            await Task.Run(async () =>
            {
                MarketApi api = new MarketApi(KeyContorller.LoadKey());
                var orders = await api.GetOrders();
                for (int i = 0; i < orders.Count; i++)
                {
                    await api.UpdateOrder(orders[i].I_classid, orders[i].I_instanceid, "1");
                }
            });
        }
    }
}
