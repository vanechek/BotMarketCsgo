using BotCsgo.Helpers;
using BotCsgo.Model.Response;
using BotCsgo.Model.Response.acc;
using BotCsgo.Model.Response.Item;
using BotCsgo.Model.Response.Orders;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace BotCsgo.Model
{
    class MarketApi
    {
        public string your_secret_key { get; set; }
        public MarketApi(string AccesKey)
        {
            your_secret_key = AccesKey;
        }
        public async Task<string> GetBalance()
        {
            var balance = await BaseGetMarketApi<get_money>.GetMarketAsyncWithData($"https://market.csgo.com/api/v2/get-money?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
            return balance.Money.ToString();
        }

        public async Task<bool> UpdateInvent()
        {
            var inventory = await BaseGetMarketApi<update_inventory>.GetMarketAsyncWithData($"https://market.csgo.com/api/v2/update-inventory/?key={your_secret_key}", your_secret_key).ConfigureAwait(false);

            return inventory.Success;
        }

        public void StartPing()
        {
            BaseGetMarketApi<marketping>.GetMarketAsync($"https://market.csgo.com/api/v2/ping?key={your_secret_key}", your_secret_key);
        }

        public async Task<GetDiscounts> GetDiscounts()
        {
            var discounts = await BaseGetMarketApi<GetDiscounts>.GetMarketAsyncWithData($"https://market.csgo.com/api/GetDiscounts/?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
            return discounts;
        }
        public void Offline()
        {
            BaseGetMarketApi<object>.GetMarketAsync($"https://market.csgo.com/api/v2/go-offline?key={your_secret_key}", your_secret_key);
        }

        public async Task<ObservableCollection<items>> GetInventoryItems()
        {
            ObservableCollection<items> items = new ObservableCollection<items>();
            var my_Inventory = await BaseGetMarketApi<my_inventory>.GetMarketAsyncWithData($"https://market.csgo.com/api/v2/my-inventory/?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
            if (my_Inventory != null)  
            {
                for (int i = 0; i < my_Inventory.Items.Length; i++)
                {
                    items.Add(my_Inventory.Items[i]);
                }
            }
            return items;
        }

        public async Task<ItemRequest> ItemReqeustOut(string botid)
        {
            var itemRequestOut = await BaseGetMarketApi<ItemRequest>.GetMarketAsyncWithData($"https://market.csgo.com/api/ItemRequest/out/{botid}/?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
            return itemRequestOut;
        }
        public async Task<ItemRequest> ItemReqeustIn(string botid)
        {
            var itemRequestIn = await BaseGetMarketApi<ItemRequest>.GetMarketAsyncWithData($"https://market.csgo.com/api/ItemRequest/in/{botid}/?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
            return itemRequestIn;
        }
        public async Task<ObservableCollection<items>> GetSellList()
        {
            ObservableCollection<items> items = new ObservableCollection<items>();
            var listItems = await BaseGetMarketApi<ListItems>.GetMarketAsyncWithData($"https://market.csgo.com/api/v2/items?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
            if (listItems != null && listItems.Items != null)
            {
                for (int i = 0; i < listItems.Items.Length; i++)
                {
                    if (listItems.Items[i].Status == "1")
                    {
                        items.Add(listItems.Items[i]);
                    }
                }
            }
            return items;
        }

        public void AddOrdersForBot(string classid, string instanceid)
        {
            try
            {
                BaseGetMarketApi<InsertOrders>.GetMarketAsync($"https://market.csgo.com/api/InsertOrder/{classid}/{instanceid}/{1}/?key={your_secret_key}", your_secret_key);
            }
            catch
            {

            }
        }
        public void AddOrders(string classid, string instanceid,string price)
        {
            try
            {
                BaseGetMarketApi<InsertOrders>.GetMarketAsync($"https://market.csgo.com/api/InsertOrder/{classid}/{instanceid}/{price}/?key={your_secret_key}", your_secret_key);
            }
            catch
            {

            }
        }
        public async Task<ObservableCollection<Offers>> GetItemsToGive()
        {
            ObservableCollection<Offers> items = new ObservableCollection<Offers>();
            GetItemsToGive  getItemsToGive;
            HttpClient client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(your_secret_key))
            {
                string responseBody = "";
                try
                {
                    var response = await client.GetAsync($"https://market.csgo.com/api/GetItemsToGive/?key={your_secret_key}");
                    if (response.IsSuccessStatusCode == true)
                    {
                        responseBody = await response.Content.ReadAsStringAsync();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                getItemsToGive = JsonConvert.DeserializeObject<GetItemsToGive>(responseBody);
                if (getItemsToGive.offers != null)
                {
                    for (int i = 0; i < getItemsToGive.offers.Length; i++)
                    {
                        items.Add(getItemsToGive.offers[i]);
                    }
                }
            }
            else
            {
                return new ObservableCollection<Offers>();
            }
            return items;
        }


        public async Task<string> GetFirstPrice(string market_hash_name)
        {
            string firstPrice = string.Empty;
            var items = await BaseGetMarketApi<search_item_by_hash_name_specific>.GetMarketAsyncWithData($"https://market.csgo.com/api/v2/search-item-by-hash-name-specific?key={your_secret_key}&hash_name={market_hash_name}", your_secret_key).ConfigureAwait(false);
            foreach (var item in items.data)
            {
                firstPrice = item.Price.ToString();
                break;
            }
            return firstPrice;
        }

        public void RemoveAllFromSale()
        {
            BaseGetMarketApi<object>.GetMarketAsync($"https://market.csgo.com/api/v2/remove-all-from-sale?key={your_secret_key}", your_secret_key);
        }

        public void AddToSale(string price, string id)
        {
            BaseGetMarketApi<add_to_sale>.GetMarketAsync($"https://market.csgo.com/api/v2/add-to-sale?key={your_secret_key}&id={id}&price={price}&cur=RUB", your_secret_key);
        }

        public void SetCurrentPrice(string item_Id, int price)
        {
            BaseGetMarketApi<SetPrice>.GetMarketAsync($"https://market.csgo.com/api/SetPrice/{item_Id}/{price}/?key={your_secret_key}", your_secret_key);
        }
        public void SetPrice(string item_id, int price)
        {
            BaseGetMarketApi<set_price>.GetMarketAsync($"https://market.csgo.com/api/v2/set-price?key={your_secret_key}&item_id={item_id}&price={price}&cur=RUB", your_secret_key);
        }

        public async Task<ItemInfo> GetItemInfo(string classid, string instanceid)
        {
            var itemInfo = await BaseGetMarketApi<ItemInfo>.GetMarketAsyncWithData($"https://market.csgo.com/api/ItemInfo/{classid}_{instanceid}/ru?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
            return itemInfo;
        }

        public async Task<GetCounters> GetCounters()
        {
            var getCounters = await BaseGetMarketApi<GetCounters>.GetMarketAsyncWithData($"https://market.csgo.com/api/GetCounters/?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
            return getCounters;
        }


        public async Task<ObservableCollection<orders>> GetOrders()
        {
            ObservableCollection<orders> items = new ObservableCollection<orders>();
            var orders = await BaseGetMarketApi<GetOrders>.GetMarketAsyncWithData($"https://market.csgo.com/api/GetOrders/0/?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
            for (int i = 0; i < orders.Orders.Length; i++)
            {
                items.Add(orders.Orders[i]);
                try
                {
                    orders.Orders[i].Info = await GetBuyOfferItem(orders.Orders[i].I_classid, orders.Orders[i].I_instanceid);
                }
                catch
                { }
            }
            return items;
        }

        public async Task<ObservableCollection<items>> GetOrdersWithStatus()
        {
            ObservableCollection<items> items = new ObservableCollection<items>();
            var listItems = await BaseGetMarketApi<ListItems>.GetMarketAsyncWithData($"https://market.csgo.com/api/v2/items?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
            for (int i = 0; i < listItems.Items.Length; i++)
            {
                if (listItems.Items[i].Status != "1")
                {
                    items.Add(listItems.Items[i]);
                    if (listItems.Items[i].Status == "2")
                    {
                        await ItemReqeustOut(listItems.Items[i].Botid);
                    }
                    if (listItems.Items[i].Status == "4")
                    {
                        await ItemReqeustIn(listItems.Items[i].Botid);
                    }
                }
            }
            return items;
        }

        public async Task<string> GetBuyOfferItem(string classid, string instanceid)
        {
            var itemInfo = await BaseGetMarketApi<ItemInfo>.GetMarketAsyncWithData($"https://market.csgo.com/api/ItemInfo/{classid}_{instanceid}/ru/?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
            buy_offers maxBuyOffer = new buy_offers();
            foreach (var item in itemInfo.Buy_offers)
            {
                maxBuyOffer = item;
                return maxBuyOffer.O_price;
            }
            return maxBuyOffer.O_price;
        }
        public void SetToken(string token)
        {
            BaseGetMarketApi<SetToken>.GetMarketAsync($"https://market.csgo.com/api/SetToken/{token}/?key={your_secret_key}", your_secret_key);
        }

        public async Task UpdateOrder(string classid, string instanceid, string price)
        {
            await BaseGetMarketApi<UpdateOrder>.GetMarketAsyncWithData($"https://market.csgo.com/api/UpdateOrder/{classid}/{instanceid}/{price}/?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
        }

        public void ProcessOrder(string classid, string instanceid, string price)
        {
            BaseGetMarketApi<ProcessOrder>.GetMarketAsync($"https://market.csgo.com/api/ProcessOrder/{classid}/{instanceid}/{price}/?key={your_secret_key}", your_secret_key);
        }

        public void DeleteOrder()
        {
            BaseGetMarketApi<DeleteOrders>.GetMarketAsync($"https://market.csgo.com/api/DeleteOrders/?key={your_secret_key}", your_secret_key);
        }

        public async Task<StatusOrders> GetStatusOrder()
        {
            var currentStatus = await BaseGetMarketApi<StatusOrders>.GetMarketAsyncWithData($"https://market.csgo.com/api/StatusOrders/?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
            return currentStatus;
        }
        public async Task<ObservableCollection<Log>> GetOrdersLog()
        {
            var getOrdersLog = await BaseGetMarketApi<GetOrdersLog>.GetMarketAsyncWithData($"https://market.csgo.com/api/GetOrdersLog/?key={your_secret_key}", your_secret_key).ConfigureAwait(false);
            ObservableCollection<Log> items = new ObservableCollection<Log>();
            if (getOrdersLog.Log != null)
            {
                for (int i = 0; i < getOrdersLog.Log.Length; i++)
                {
                    items.Add(getOrdersLog.Log[i]);
                }
            }
            else
            {
                return items = null;
            }
            return items;
        }
    }
}

