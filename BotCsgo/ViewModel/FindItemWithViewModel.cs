using BotCsgo.Controller;
using BotCsgo.Helpers;
using BotCsgo.Helpers.Parser;
using BotCsgo.Helpers.Parser.Steam;
using BotCsgo.Model;
using BotCsgo.Model.Response;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace BotCsgo.ViewModel
{

    public class FindItemWithViewModel : BaseViewModel
    {
        #region Binding

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value; OnPropertyChanged();
            }
        }
        private Visibility _StickerPriceVisiability;
        public Visibility StickerPriceVisiability
        {
            get { return _StickerPriceVisiability; }
            set
            {
                _StickerPriceVisiability = value; OnPropertyChanged();
            }
        } 
        private Visibility _PricesVisiability;
        public Visibility PricesVisiability
        {
            get { return _PricesVisiability; }
            set
            {
                _PricesVisiability = value; OnPropertyChanged();
            }
        }
        private string _NameItem;
        public string NameItem
        {
            get { return _NameItem; }
            set
            {
                _NameItem = value; OnPropertyChanged();
            }
        } 
        private string _SumPricesStickers;
        public string SumPricesStickers
        {
            get { return _SumPricesStickers; }
            set
            {
                _SumPricesStickers = value; OnPropertyChanged();
            }
        }
        private object _SelectedCategory = "Без стикеров";
        public object SelectedCategory
        {
            get { return _SelectedCategory; }
            set
            {
                _SelectedCategory = value; OnPropertyChanged();
            }
        }
        private string _Path;
        public string Path
        {
            get { return _Path; }
            set
            {
                _Path = value; OnPropertyChanged();
            }
        }
        private string _PriceSticker;
        public string PriceSticker
        {
            get { return _PriceSticker; }
            set
            {
                _PriceSticker = value; OnPropertyChanged();
            }
        }
        private string _MarketCsgoPrice;
        public string MarketCsgoPrice
        {
            get { return _MarketCsgoPrice; }
            set
            {
                _MarketCsgoPrice = value; OnPropertyChanged();
            }
        }
        private string _SteamPrice;
        public string SteamPrice
        {
            get { return _SteamPrice; }
            set
            {
                _SteamPrice = value; OnPropertyChanged();
            }
        }
        private string _HisPrice;
        public string HisPrice
        {
            get { return _HisPrice; }
            set
            {
                _HisPrice = value; OnPropertyChanged();
            }
        }
        private ObservableCollection<items> _ListItems;
        public ObservableCollection<items> ListItems
        {
            get { return _ListItems; }
            set { _ListItems = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Sticker> _StickerList;
        public ObservableCollection<Sticker> StickerList
        {
            get { return _StickerList; }
            set { _StickerList = value; OnPropertyChanged(); }
        }
        #endregion
        private ParserWorker<ObservableCollection<items>> parserWorker;
        private MarketApi marketApi;
        public FindItemWithViewModel()
        {
            PricesVisiability = Visibility.Hidden;
            StickerPriceVisiability = Visibility.Hidden;
            marketApi = new MarketApi(KeyContorller.LoadKey());
            parserWorker = new ParserWorker<ObservableCollection<items>>(new ParserBuyItems());
            ListItems = new ObservableCollection<items>();
            parserWorker.OnNewData += (arg, collection) =>
            {
                ListItems = collection;
              
            };
        }

        public ICommand ClickToParse => new RelayCommand((obj) =>
        {
            var category = SelectedCategory as ComboBoxItem;
            int numberCategory = 0;
            numberCategory = category.Content switch
            {
                "Без стикеров" => 0,
                "С стикерами" => -1,
                "С одинаковыми стикерами" => -2,
                _ => 0,
            };
            parserWorker.Settings = new BuyItemsSettings(1, 100000, string.Empty, 1, numberCategory);
            parserWorker.StartOnce();
        });
        public items SelectedItem { get; set; }
        public ICommand ClickToFindInfo => new RelayCommand(async(obj) =>
        {
            PricesVisiability = Visibility.Visible;
            StickerPriceVisiability = Visibility.Visible;
            var item = obj as items;
            var itemInfo = await marketApi.GetItemInfo(item.Classid, item.Instanceid);
            ParserWorker<string> parserWorker = new ParserWorker<string>(new SteamPriceParser());
            parserWorker.Settings = new SteamPriceSettings(itemInfo.Market_hash_name);
            parserWorker.StartOnce();
            parserWorker.OnNewData += (arg, price) =>
            {
                ((items)obj).SteamPrice= price;
            };
            MarketCsgoPrice = itemInfo.Min_price;
            foreach (var currentItem in ((items)obj).Stickers)
            {
                parserWorker.Settings = new SteamPriceSettings(currentItem.Title);
                parserWorker.StartOnce();
                parserWorker.OnNewData += (arg, price) =>
                {
                    currentItem.Price = price;
                };
            }
        });
    }
}
