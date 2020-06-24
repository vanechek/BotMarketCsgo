using BotCsgo.Controller;
using BotCsgo.Helpers.Parser;
using BotCsgo.Model;
using BotCsgo.Model.Response;
using BotCsgo.Model.Response.Item;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BotCsgo.ViewModel
{
    public class MyInventoryViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Binding
        private int _CountSellItem;
        public int CountSellItem
        {
            get => _CountSellItem;
            set { _CountSellItem = value; OnPropertyChanged(); }
        }
        private string _FeeOnSale;
        public string FeeOnSale
        {
            get => _FeeOnSale;
            set { _FeeOnSale = value; OnPropertyChanged(); }
        }
        private string _YourDiscounts;
        public string YourDiscounts
        {
            get => _YourDiscounts;
            set
            {
                _YourDiscounts = value; OnPropertyChanged();
            }
        }
        private string _ImageInventoryItem;
        public string ImageInventoryItem
        {
            get { return _ImageInventoryItem; }
            set
            {
                _ImageInventoryItem = value; OnPropertyChanged();
            }
        }
        private string _InfoItem;
        public string InfoItem
        {
            get { return _InfoItem; }
            set { _InfoItem = value; OnPropertyChanged(); }
        }
        private bool _MainContentIsEnabled;
        public bool MainContentIsEnabled
        {
            get { return _MainContentIsEnabled; }
            set { _MainContentIsEnabled = value; OnPropertyChanged(); }
        }
        private double _MainContentOpacity;
        public double MainContentOpacity
        {
            get { return _MainContentOpacity; }
            set { _MainContentOpacity = value; OnPropertyChanged(); }
        }
        private string _ItemName;
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; OnPropertyChanged(); }
        }
        private string _YourPriceItem;
        public string YourPriceItem
        {
            get { return _YourPriceItem; }
            set { _YourPriceItem = value; OnPropertyChanged(); }
        }

        private string _RecommendedPrice;
        public string RecommendedPrice
        {
            get { return _RecommendedPrice; }
            set { _RecommendedPrice = value; OnPropertyChanged(); }
        }
        private string _MinimalPriceItem;
        public string MinimalPriceItem
        {
            get { return _MinimalPriceItem; }
            set { _MinimalPriceItem = value; OnPropertyChanged(); }
        }
        private string _PriceForFastSells;
        public string PriceForFastSells
        {
            get { return _PriceForFastSells; }
            set { _PriceForFastSells = value; OnPropertyChanged(); }
        }
        private string _DiscountsPriceItem;
        public string DiscountsPriceItem
        {
            get { return _DiscountsPriceItem; }
            set { _DiscountsPriceItem = value; OnPropertyChanged(); }
        }

        private Visibility _InventoryContorlVisibility;
        public Visibility InventoryContorlVisibility
        {
            get { return _InventoryContorlVisibility; }
            set
            {
                _InventoryContorlVisibility = value; OnPropertyChanged();
            }
        }
        private Visibility _InfoItemContorlVisiability;
        public Visibility InfoItemContorlVisiability
        {
            get { return _InfoItemContorlVisiability; }
            set
            {
                _InfoItemContorlVisiability = value; OnPropertyChanged();
            }
        }
        private int _CountItemsOnSale;
        public int CountItemsOnSale
        {
            get { return _CountItemsOnSale; }
            set { _CountItemsOnSale = value; OnPropertyChanged(); }
        }
        private int _CountOrders;
        public int CountOrders
        {
            get { return _CountOrders; }
            set { _CountOrders = value; OnPropertyChanged(); }
        }
        private int _CountNotify;
        public int CountNotify
        {
            get { return _CountNotify; }
            set { _CountNotify = value; OnPropertyChanged(); }
        }
        private string _DateRefresh;
        public string DateRefresh
        {
            get { return _DateRefresh; }
            set { _DateRefresh = value; OnPropertyChanged(); }
        }

        private ObservableCollection<items> _SellItems;
        public ObservableCollection<items> SellItems
        {
            get { return _SellItems; }
            set { _SellItems = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Offers> _GetOffers;
        public ObservableCollection<Offers> GetOffers
        {
            get { return _GetOffers; }
            set { _GetOffers = value; OnPropertyChanged(); }
        }
        private ObservableCollection<items> _InventoryItems;
        public ObservableCollection<items> InventoryItems
        {
            get { return _InventoryItems; }
            set { _InventoryItems = value; OnPropertyChanged(); }
        }
        #endregion
        #region Commands
        public ICommand Refresh => new RelayCommand(async (obj) =>
        {
            await Task.Run(async () =>
            {
                try
                {
                    var items = await api.UpdateInvent();
                    SellItems = await api.GetSellList();
                    DateRefresh = $"обновлено в {DateTime.Now.Date.ToString(CultureInfo.CurrentCulture)}";
                }
                catch (AggregateException)
                {
                    MessageBox.Show("Ошибка обновления");
                }
            }).ConfigureAwait(true);
        }, (obj) => !string.IsNullOrEmpty(KeyContorller.LoadKey()));

        public ICommand StopSell => new RelayCommand((obj) => api.Offline(), (obj) => !string.IsNullOrEmpty(KeyContorller.LoadKey()));
        public ICommand RemoveAllSellItem => new RelayCommand((obj) => ClickToRemoveAllSellItems(), (obj) => !string.IsNullOrEmpty(KeyContorller.LoadKey()));
        public ICommand OpenInvent => new RelayCommand((obj) => ClickToOpenInvent(), (obj) => !string.IsNullOrEmpty(KeyContorller.LoadKey()));
        public ICommand Reprice => new RelayCommand(async (obj) =>
        {
            await Task.Run(async () =>
            {
                try
                {
                    var price = await api.GetInventoryItems();
                    for (int i = 0; i < price.Count; i++)
                    {
                        var firstPrice = await api.GetFirstPrice(price[i].Market_hash_name);
                        var currnetPrice = price[i].Price;
                        if (currnetPrice.Replace(".", "") == firstPrice)
                        {
                            continue;
                        }
                        else
                        {
                            if (price[i].Position > 1)
                            {
                                api.SetPrice(price[i].Item_id, Convert.ToInt32(firstPrice, CultureInfo.CurrentCulture) - 1);
                            }
                        }
                        Thread.Sleep(150);
                    }
                }
                catch
                {
                    api.StartPing();
                }
                Thread.Sleep(TimeSpan.FromSeconds(20));
            });
        }, (obj) => !string.IsNullOrEmpty(KeyContorller.LoadKey()));
        public ICommand ClickOpenItemInfoControl => new RelayCommand((obj) => OpenItemInfoControl(obj));
        public ICommand SellСurrentItem => new RelayCommand(async (obj) =>
        {
            try
            {
                var PriceBox = obj as TextBox;
                CurrentItem.Price = PriceBox.Text;
                await Task.Run(() =>
                {
                    if (CurrentItem.Item_id == null)
                    {
                        api.AddToSale(CurrentItem.Price.Replace(",", ""), CurrentItem.Id);
                        CloseItemInfoControl();
                    }
                    else
                    {
                        api.SetPrice(CurrentItem.Item_id, Convert.ToInt32(CurrentItem.Price.Replace(".", ""), CultureInfo.CurrentCulture));
                        CloseItemInfoControl();
                    }
                });
                InventoryItems = await api.GetInventoryItems();
                SellItems = await api.GetSellList();
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        });
        public ICommand ClickCloseItemInfoControl => new RelayCommand((obj) => CloseItemInfoControl());
        public ICommand ClickCloseInventoryControl => new RelayCommand((obj) => CloseInventoryControl());
        public ICommand NoSell => new RelayCommand(async (obj) =>
        {
            var item = obj as items;
            api.SetPrice(CurrentItem.Item_id, 0);
            SellItems = await api.GetSellList();
            CloseItemInfoControl();
        });
        public ICommand AutoSellItems => new RelayCommand(async (obj) =>
        {
            await Task.Run(async () =>
            {
                for (int i = 0; i < ListSellItems.Count; i++)
                {
                    try
                    {
                        var currentFirstPrice = await api.GetFirstPrice(ListSellItems[i].Market_hash_name);
                        int currentPriceItem = Convert.ToInt32(currentFirstPrice) - 1;
                        api.AddToSale(currentPriceItem.ToString(), ListSellItems[i].Id);
                        CountSellItem--;
                        InventoryItems = await api.GetInventoryItems();
                    }
                    catch
                    {
                        continue;
                    }
                }
            }).ConfigureAwait(false);
        });
        public ICommand AddInList => new RelayCommand((obj) =>
        {
            var item = obj as items;
            ListSellItems.Add(item);
            CountSellItem++;
        });
        public ICommand ResetSellItem => new RelayCommand((obj) =>
        {
            for (int i = 0; i < ListSellItems.Count; i++)
            {
                ListSellItems.RemoveAt(i);
            };
            CountSellItem = 0;
        });
        #endregion
        private MarketApi api;
        public items CurrentItem { get; set; }
        public List<items> ListSellItems { get; set; }
        public string PriceItem { get; set; }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string newPrice = null;
                switch (columnName)
                {
                    case "YourPriceItem":
                        if (PriceItem == YourPriceItem)
                        {
                            return PriceItem;
                        }
                        else
                        {
                            newPrice = UpdateDiscountPrice(YourPriceItem);
                        }
                        break;
                }
                return newPrice;
            }
        }

        public MyInventoryViewModel()
        {
            StartViewModel();
        }
        public string UpdateDiscountPrice(string price)
        {
            DiscountsPriceItem = (BuyItemsViewModel.GetDoublePrice(price) * (1 - Convert.ToDouble(FeeOnSale.Replace("%", ""), NumberFormatInfo.InvariantInfo) / 100.00)).ToString(".00");
            return DiscountsPriceItem;
        }
        private void StartViewModel()
        {
            MainContentOpacity = 1;
            MainContentIsEnabled = true;
            InfoItemContorlVisiability = Visibility.Hidden;
            InventoryContorlVisibility = Visibility.Hidden;
            CountItemsOnSale = 0;
            CountOrders = 0;
            CountNotify = 0;
            api = new MarketApi(KeyContorller.LoadKey());
            var discounts = api.GetDiscounts();
            Thread.Sleep(150);
            if (discounts.Result != null)
            {
                FeeOnSale = discounts.Result.discounts.sell_fee;
                YourDiscounts = discounts.Result.discounts.buy_discount;
            }
            SellItems = new ObservableCollection<items>();
            ListSellItems = new List<items>();
        }

        public async void CloseItemInfoControl()
        {
            await Task.Run(() =>
            {
                if (MainContentOpacity >= 0.2 && InventoryContorlVisibility == Visibility.Hidden)
                {
                    for (double i = 0.2; i <= 1.1; i += 0.1)
                    {
                        MainContentOpacity = i;
                        Thread.Sleep(50);
                    }
                }
                InfoItemContorlVisiability = Visibility.Hidden;
                MainContentIsEnabled = true;
                ClearItemInfoControl();
            }).ConfigureAwait(false);
        }
        public async void CloseInventoryControl()
        {
            await Task.Run(() =>
            {
                if (MainContentOpacity <= 0.3)
                {
                    for (double i = 0.2; i <= 1.1; i += 0.1)
                    {
                        MainContentOpacity = i;
                        Thread.Sleep(50);
                    }
                }
                InventoryContorlVisibility = Visibility.Hidden;
                MainContentIsEnabled = true;
                ClearItemInfoControl();
            }).ConfigureAwait(false);
        }
        public void ClearItemInfoControl()
        {
            ItemName = string.Empty;
            DiscountsPriceItem = string.Empty;
            PriceForFastSells = string.Empty;
            MinimalPriceItem = string.Empty;
            RecommendedPrice = string.Empty;
            YourPriceItem = string.Empty;
        }
        public async void OpenItemInfoControl(object obj)
        {
            await Task.Run(async () =>
            {
                var currentItem = obj as items;
                CurrentItem = currentItem;
                if (MainContentOpacity >= 1)
                {
                    for (double i = 1.0; i > 0.2; i -= 0.1)
                    {
                        MainContentOpacity = i;
                        Thread.Sleep(50);
                    }
                }
                InfoItemContorlVisiability = Visibility.Visible;
                MainContentIsEnabled = false;
                try
                {
                    var ItemInfo = await api.GetItemInfo(currentItem.Classid, currentItem.Instanceid);

                    string firstOffer = null;
                    string firstBuyOffer = null;
                    if (ItemInfo.Offers != null)
                    {
                        for (int i = 0; i < ItemInfo.Offers.Length;)
                        {
                            firstOffer = ItemInfo.Offers[i].Price;
                            break;
                        }
                    }
                    else
                    {
                        firstOffer = "0";
                    }
                    if (ItemInfo.Buy_offers != null)
                    {
                        for (int i = 0; i < ItemInfo.Buy_offers.Length;)
                        {
                            firstBuyOffer = ItemInfo.Buy_offers[i].O_price;
                            break;
                        }
                    }
                    else
                    {
                        firstBuyOffer = "0";
                    }
                    MinimalPriceItem = await api.GetFirstPrice(ItemInfo.Market_hash_name);
                    DiscountsPriceItem = (BuyItemsViewModel.GetDoublePrice(MinimalPriceItem) * (1 - Convert.ToDouble(FeeOnSale.Replace("%", ""), NumberFormatInfo.InvariantInfo) / 100.00)).ToString(".00");
                    PriceItem = DiscountsPriceItem;
                    ItemName = ItemInfo.Market_name;
                    YourPriceItem = BuyItemsViewModel.GetDoublePrice(MinimalPriceItem).ToString(".00");
                    RecommendedPrice = firstOffer;
                    PriceForFastSells = firstBuyOffer;
                    MainContentIsEnabled = false;
                }
                catch
                {
                    ClearItemInfo();
                    ItemName = "Не удалось загрузить информацию предмета";
                }
            });
        }
        public void ClearItemInfo()
        {
            YourDiscounts = "";
            YourPriceItem = "";
            PriceForFastSells = "";
            RecommendedPrice = "";
        }

        public void ClickToRemoveAllSellItems()
        {
            api.RemoveAllFromSale();
        }

        public async void ClickToOpenInvent()
        {
            await Task.Run(async () =>
                {
                    api = new MarketApi(KeyContorller.LoadKey());
                    for (double i = 1.0; i > 0.2; i -= 0.1)
                    {
                        MainContentOpacity = i;
                        Thread.Sleep(20);
                    }
                    InventoryContorlVisibility = Visibility.Visible;
                    MainContentIsEnabled = false;
                    InventoryItems = await api.GetInventoryItems();
                });
        }

        public async void GetCounters()
        {
            var counters = await api.GetCounters();
            if (counters != null)
            {
                CountItemsOnSale = counters.Items_on_sale;
                CountOrders = counters.Orders;
                CountNotify = counters.Notify;
            }
        }

        public static string ParseDoubleInString(double name)
        {
            if (name != 0)
            {
                return default;
            }
            string data = Convert.ToString(name, CultureInfo.CurrentCulture);
            return data;
        }
    }
}
