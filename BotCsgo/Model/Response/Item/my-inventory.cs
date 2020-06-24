using BotCsgo.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BotCsgo.Model.Response
{
    public class my_inventory
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("items")]
        public items[] Items { get; set; }
    }

    public class items : BaseViewModel
    {
        [JsonProperty]
        public string Id { get; set; }
        [JsonProperty("classid")]
        public string Classid { get; set; }
        [JsonProperty("instanceid")]
        public string Instanceid { get; set; }
        [JsonProperty("market_hash_name")]
        public string Market_hash_name { get; set; }
        [JsonProperty("market_price")]
        public double Market_price { get; set; }
        [JsonProperty("tradable")]
        public int Tradable { get; set; }
        [JsonProperty("item_id")]
        public string Item_id { get; set; }
        [JsonProperty("assetid")]
        public string Assetid { get; set; }
        [JsonProperty("real_instance")]
        public string Real_instance { get; set; }
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("price")]
        public string Price { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("live_time")]
        public string Live_time { get; set; }
        [JsonProperty("left")]
        public string Left { get; set; }
        [JsonProperty("botid")]
        public string Botid { get; set; }
        [JsonProperty("buy_order")]
        public string Buy_order { get; set; }
        [JsonProperty("avg_price")]
        public string Avg_price { get; set; }
        [JsonProperty("popularity_7d")]
        public string Popularity_7d { get; set; }
        public string Path { get; set; }
        public string urlItem { get; set; }
        public ObservableCollection<Sticker> Stickers { get; set; }
        public string StickerPath { get; set; }
        public string Title { get; set; }
        public string SteamPrice { get; set; }
    }

    public class Sticker
    {
        public string Path { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
    }
    public class ClassDynamic
    {
        public string price { get; set; }
        public string buy_order { get; set; }
        public string avg_price { get; set; }
        public string popularity_7d { get; set; }
        public string market_hash_name { get; set; }
    }
}
