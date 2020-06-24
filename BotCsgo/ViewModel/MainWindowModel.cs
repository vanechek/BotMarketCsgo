using BotCsgo.Controller;
using BotCsgo.Model;
using BotCsgo.Pages;
using BotCsgo.Service;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BotCsgo.ViewModel
{
    public class MainWindowModel : BaseViewModel, IDataErrorInfo
    {
        #region Binding

        private string _ApiKey;
        public string ApiKey
        {
            get
            {
                return _ApiKey;
            }
            set
            {
                _ApiKey = value; OnPropertyChanged();
            }
        }
        private string _Balance;
        public string Balance
        {
            get { return _Balance; }
            set { _Balance = value; OnPropertyChanged(); }
        }
        private string _KeyIsFinded;
        public string KeyIsFinded
        {
            get { return _KeyIsFinded; }
            set { _KeyIsFinded = value; OnPropertyChanged(); }
        }
        public string Error => throw new NotImplementedException();
        #endregion
        #region Настройка окна
        public double WindowMinHeight { get; set; } = 858;
        public double WindowMinWidth { get; set; } = 1003;

        public int cornerRadius { get; set; } = 10;
        public int CornerRadius
        {
            get { return mWindow.WindowState == WindowState.Maximized ? 0 : cornerRadius; }
            set { cornerRadius = value; }
        }


        public CornerRadius WindowCornerRadius { get { return new CornerRadius(CornerRadius); } }

        public double WindowCaptionHeight { get; set; } = 50;

        private int ResizeBorderThickness { get; set; } = 6;
        public Thickness WindowResizeBorderThickness { get { return new Thickness(ResizeBorderThickness); } }

        public int titleHeightLenght = 50;
        public GridLength TitleHeightLenght { get { return new GridLength(titleHeightLenght); } }

        private Window mWindow;
        #endregion
        private Page currentPage;
        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value; OnPropertyChanged();
            }
        }
        public PageService pageService;
        public MainWindowModel(Window window, PageService pageService)
        {
            this.pageService = pageService;
            mWindow = window;
            mWindow.StateChanged += (seder, e) =>
            {
                OnPropertyChanged(nameof(WindowCaptionHeight));
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(WindowCornerRadius));
                OnPropertyChanged(nameof(WindowMinHeight));
                OnPropertyChanged(nameof(WindowMinWidth));
            };

            CloseWindow = new RelayCommand((obj) => ClickToWindowClose());
            FullScreenWindow = new RelayCommand((obj) => ClickToFullScreenWindow());
            HideWindow = new RelayCommand((obj) => ClickToHideWindow());
            OpenMyInventoryView = new RelayCommand((obj) => pageService.ChangePage(new MyInventoryPage()));
            OpenBotPage = new RelayCommand((obj) => pageService.ChangePage(new BotPage()));
            OpenBuyItemsPage = new RelayCommand((obj) => pageService.ChangePage(new BuyItemsPage()));
            OpenItemWithSticker = new RelayCommand((obj) => pageService.ChangePage(new FindItemWithPage()));
            pageService.OnPageChanged += (page) =>
            {
                CurrentPage = page;
            };
            pageService.ChangePage(new MyInventoryPage());
        }

        public ICommand CloseWindow { get; }
        public ICommand FullScreenWindow { get; }
        public ICommand HideWindow { get; }
        public ICommand OpenAutoBuyView { get; }
        public ICommand OpenMyInventoryView { get; }
        public ICommand OpenBotPage { get; }
        public ICommand OpenBuyItemsPage { get; }
        public ICommand SellItem { get; }
        public ICommand OpenItemWithSticker { get; }
        public void ClickToWindowClose()
        {
            if (mWindow.IsActive == true)
            {
                mWindow.Close();
            }
        }
        public void ClickToFullScreenWindow()
        {
            if (mWindow.IsActive == true && mWindow.WindowState == WindowState.Normal)
            {
                mWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                mWindow.WindowState = WindowState.Normal;
            }
        }


        public void ClickToHideWindow()
        {
            if (mWindow.IsActive == true)
            {
                mWindow.WindowState = WindowState.Minimized;
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "ApiKey":
                        if (!string.IsNullOrEmpty(ApiKey))
                        {
                            MarketApi marketApi = new MarketApi(ApiKey);
                            try
                            {
                                marketApi.StartPing();
                                var balance = marketApi.GetBalance();

                                KeyIsFinded = "Ключ валидный";
                                Balance = balance.Result;
                                KeyContorller.SaveKey(ApiKey);
                            }
                            catch
                            {
                                KeyIsFinded = "Ключ не валидный";
                                Balance = "";
                            }
                            break;
                        }
                        else
                        {
                            var ApiKeyWithFile = KeyContorller.LoadKey();
                            if (string.IsNullOrWhiteSpace(ApiKey))
                            {
                                ApiKey = ApiKeyWithFile;
                            }
                        }
                        break;
                }
                return _KeyIsFinded;
            }
        }

    }
}
