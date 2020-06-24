using BotCsgo.Controller;
using BotCsgo.Helpers.Parser;
using BotCsgo.Model;
using BotCsgo.Model.Response;
using HtmlAgilityPack;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BotCsgo.ViewModel
{
    public class BuyItemsViewModel : BaseViewModel
    {
        #region Binding
        private string _UrlImage;
        public string UrlImage
        {
            get { return _UrlImage; }
            set { _UrlImage = value; OnPropertyChanged(); }
        }
        private string _SelectedCategory;
        public string SelectedCategory
        {
            get { return _SelectedCategory; }
            set { _SelectedCategory = value; OnPropertyChanged(); }
        }
        private string _Url;
        public string Url
        {
            get { return _Url; }
            set { _Url = value; OnPropertyChanged(); }
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
        private Visibility _InfoItemContorlVisiability;
        public Visibility InfoItemContorlVisiability
        {
            get { return _InfoItemContorlVisiability; }
            set
            {
                _InfoItemContorlVisiability = value; OnPropertyChanged();
            }
        }
        private string _MinPriceOrder;
        public string MinPriceOrder
        {
            get { return _MinPriceOrder; }
            set { _MinPriceOrder = value; OnPropertyChanged(); }
        }
        private string _SearchItem;
        public string SearchItem
        {
            get { return _SearchItem; }
            set { _SearchItem = value; OnPropertyChanged(); }
        }
        private int _MinPrice;
        public int MinPrice
        {
            get { return _MinPrice; }
            set { _MinPrice = value; OnPropertyChanged(); }
        }
        private int _MaxPrice;
        public int MaxPrice
        {
            get { return _MaxPrice; }
            set { _MaxPrice = value; OnPropertyChanged(); }
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
        private string _ItemName;
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; OnPropertyChanged(); }
        }
        private string _DiscountsPriceItem;
        public string DiscountsPriceItem
        {
            get { return _DiscountsPriceItem; }
            set { _DiscountsPriceItem = value; OnPropertyChanged(); }
        }
        private bool _MainContentIsEnabled;
        public bool MainContentIsEnabled
        {
            get { return _MainContentIsEnabled; }
            set { _MainContentIsEnabled = value; OnPropertyChanged(); }
        }
        private int _CurrentPagee;
        public int CurrentPage
        {
            get { return _CurrentPagee; }
            set { _CurrentPagee = value; OnPropertyChanged(); }
        }
        private string _CountPage;
        public string CountPage
        {
            get { return _CountPage; }
            set { _CountPage = value; OnPropertyChanged(); }
        }
        private double _MainContentOpacity;
        public double MainContentOpacity
        {
            get { return _MainContentOpacity; }
            set { _MainContentOpacity = value; OnPropertyChanged(); }
        }
        #endregion
        #region Commands
        public ICommand ClickToOpenItemInBrowser => new RelayCommand((obj) => Process.Start(Url));
        public ICommand ClickPreviousPage => new RelayCommand((obj) => Update(CurrentPage -= 1, MinPrice, MaxPrice, SearchItem));
        public ICommand ClickNextPage => new RelayCommand((obj) => Update(CurrentPage += 1, MinPrice, MaxPrice, SearchItem));
        public ICommand PressToEnter => new RelayCommand((obj) => Update(CurrentPage, MinPrice, MaxPrice, SearchItem));
        public ICommand ClickOpenItemInfoControl => new RelayCommand((obj) => OpenItemInfoControl(obj));
        public ICommand ClickCloseItemInfoControl => new RelayCommand((obj) => CloseItemInfoControl());
        public ICommand SellСurrentItem => new RelayCommand((obj) =>
            {
                var PriceBox = obj as TextBox;

                try
                {
                    if (PriceBox.Text.Contains(","))
                    {
                        PriceBox.Text.Replace(",", "");
                    }
                    api.AddOrders(CurrentItem.Classid, CurrentItem.Instanceid, PriceBox.Text);
                    CloseItemInfoControl();
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            });
        #endregion
        public items CurrentItem { get; set; }
        private ObservableCollection<items> _ListBuyItems;
        private MarketApi api { get; set; }
        private ParserWorker<ObservableCollection<items>> parserWorker;
        public BuyItemsViewModel()
        {
            StartViewModel();
            api = new MarketApi(KeyContorller.LoadKey());
            parserWorker = new ParserWorker<ObservableCollection<items>>(new BuyItemsSettings(MinPrice, MaxPrice, SearchItem, CurrentPage, SelectedCategory == "Без стикеров" ? 1 : 0), new ParserBuyItems());
            parserWorker.OnNewData += ParserWorker_OnNewData;
            Update();
        }
        private void StartViewModel()
        {
            InfoItemContorlVisiability = Visibility.Hidden;
            MinPrice = 0;
            MaxPrice = 100000;
            CurrentPage = 1;
        }
        private void ParserWorker_OnNewData(object arg1, ObservableCollection<items> arg2)
        {
            ListBuyItems = arg2;
        }

        public ObservableCollection<items> ListBuyItems
        {
            get { return _ListBuyItems; }
            set { _ListBuyItems = value; OnPropertyChanged(); }
        }
        public void Update(int page = 1, int min = 0, int max = 100000, string searchItem = "", int sticker = 0)
        {
            parserWorker.Settings = new BuyItemsSettings(min, max, searchItem, page, sticker);
            parserWorker.StartOnce();
        }

        private void ClearItemInfoControl()
        {
            ItemName = string.Empty;
            DiscountsPriceItem = string.Empty;
            PriceForFastSells = string.Empty;
            MinimalPriceItem = string.Empty;
            RecommendedPrice = string.Empty;
            YourPriceItem = string.Empty;
            Url = string.Empty;
            UrlImage = string.Empty;
        }
        public async void CloseItemInfoControl()
        {
            await Task.Run(() =>
            {
                if (MainContentOpacity >= 0.2)
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
                try
                {
                    var ItemInfo = await api.GetItemInfo(currentItem.Classid, currentItem.Instanceid);
                    var YourCounters = await api.GetDiscounts();
                    string firstOffer = null;
                    string firstBuyOffer = null;
                    if (ItemInfo.Offers != null)
                    {
                        firstOffer = ItemInfo.Offers.FirstOrDefault().Price;
                    }
                    else
                    {
                        firstOffer = "0";
                    }
                    if (ItemInfo.Buy_offers != null)
                    {
                        firstBuyOffer = ItemInfo.Buy_offers.FirstOrDefault().O_price;
                    }
                    else
                    {
                        firstBuyOffer = "0";
                    }
                    ItemName = ItemInfo.Market_hash_name;
                    UrlImage = currentItem.Path;
                    Url = currentItem.urlItem;
                    YourPriceItem = firstBuyOffer;
                    MinimalPriceItem = ItemInfo.Min_price;

                    var Comissions = 1 - Convert.ToDouble(YourCounters.discounts.sell_fee.Replace("%", ""), NumberFormatInfo.InvariantInfo) / 100.00;
                    var PriceItemWithComissions = GetDoublePrice(MinimalPriceItem) * Comissions;
                    var profit = PriceItemWithComissions - GetDoublePrice(firstBuyOffer);

                    DiscountsPriceItem = profit.ToString(".00");
                    RecommendedPrice = firstOffer;
                    PriceForFastSells = firstBuyOffer;
                }
                catch
                {
                    ClearItemInfoControl();
                    ItemName = "Не удалось загрузить информацию предмета";
                }
                MainContentIsEnabled = false;
            });
        }
        public static double GetDoublePrice(string price)
        {
            if (!string.IsNullOrEmpty(price))
            {
                if (price.Contains("00") == true)
                {
                    return Convert.ToDouble(price.Replace("00", ""), NumberFormatInfo.InvariantInfo);
                }
                int charDouble = price.Length - 2;
                string newPrice = string.Empty;
                bool dot = false;
                int countNumberAfterPoint = 0;
                for (int i = 0; i < price.Length; i++)
                {
                    if (i == charDouble)
                    {
                        newPrice += ".";
                        dot = true;
                    }
                    if (dot)
                    {
                        countNumberAfterPoint++;
                    }
                    if (countNumberAfterPoint == 2)
                    {
                        break;
                    }
                    newPrice += price[i];
                }
                return Convert.ToDouble(newPrice, NumberFormatInfo.InvariantInfo);
            }
            return 0.0;
        }
    }
}
