using BotCsgo.Controller;
using BotCsgo.Model;
using BotCsgo.Model.Response.Item;
using BotCsgo.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BotCsgo.UserControls.Notice
{
    class ListNoticeDesignModel : BaseViewModel
    {
        #region Binding
        private double _ItemNoticeOpacity;
        public double ItemNoticeOpacity
        {
            get => _ItemNoticeOpacity;
            set
            {
                _ItemNoticeOpacity = value; OnPropertyChanged();
            }
        }
        private ObservableCollection<Offers> _ListNotice;
        public ObservableCollection<Offers> ListNotice
        {
            get
            {
                return _ListNotice;
            }
            set
            {
                _ListNotice = value; OnPropertyChanged();
            }
        }
        #endregion
        public ICommand ClickToCloseAllNotice { get; }
        public ICommand CloseNotice { get; }
        public ICommand ClickToSendItem { get; }
        public static ListNoticeDesignModel Instance
        {
            get { return new ListNoticeDesignModel(); }
        }
        public List<Offers> ClickOffers { get; set; }
        public event Action<Offers> ItemSell;
        public ListNoticeDesignModel()
        {
            ListNotice = new ObservableCollection<Offers>();
            ClickOffers = new List<Offers>();
            ClickToCloseAllNotice = new RelayCommand((obj) => CloseAllNotice(), (button) => ListNotice.Count > 1);
            CloseNotice = new RelayCommand((obj) => CloseCurrentNotice(obj));
            OnItemSell();
            ItemSell += (offers) =>
            {
                ListNotice.Add(offers);
            };
            ClickToSendItem = new RelayCommand((obj) => SendItem(obj));
        }
        public void CloseCurrentNotice(object obj)
        {
            var notice = obj as Offers;
            ClickOffers.Add(notice);
            ListNotice.Remove(notice);
        }
        public async void SendItem(object obj)
        {
            var CurrentOffer = obj as Offers;
            MarketApi api = new MarketApi(KeyContorller.LoadKey());
            await api.ItemReqeustIn(CurrentOffer.ui_bid);
            ClickOffers.Add(CurrentOffer);
        }
        public void CloseAllNotice()
        {
            var maxNotice = ListNotice.Count - 1;
            for (int i = maxNotice; i <= ListNotice.Count; i--)
            {
                if (ListNotice.Count == 0)
                {
                    break;
                }
                ClickOffers.Add(ListNotice[i]);
                ListNotice.Remove(ListNotice[i]);
            }
        }

        public async void OnItemSell()
        {
            MarketApi api = new MarketApi(KeyContorller.LoadKey());

            while (true)
            {
                if (!string.IsNullOrWhiteSpace(api.your_secret_key))
                {

                    try
                    {
                        var noticeOffers = await api.GetItemsToGive();
                        if (noticeOffers.Count >= 1)
                        {
                            for (int i = 0; i < noticeOffers.Count; i++)
                            {
                                if (ListNotice.Count > 0)
                                {

                                    if (CheckListNoticeItem(noticeOffers[i]))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (CheckIsClickListItem(noticeOffers[i]))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            ItemSell?.Invoke(noticeOffers[i]);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(noticeOffers[i].i_market_name))
                                    {
                                        if (CheckIsClickListItem(noticeOffers[i]))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            ItemSell?.Invoke(noticeOffers[i]);
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        await Task.Delay(TimeSpan.FromSeconds(5));
                    }
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }
        }

        public bool CheckListNoticeItem(Offers offers)
        {
            for (int i = 0; i < ListNotice.Count; i++)
            {
                if (offers.i_classid == ListNotice[i].i_classid)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckIsClickListItem(Offers offers)
        {
            for (int i = 0; i < ClickOffers.Count; i++)
            {
                if (offers.i_classid == ClickOffers[i].i_classid)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
