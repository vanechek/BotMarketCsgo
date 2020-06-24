using Newtonsoft.Json;

namespace BotCsgo.Model.Response
{
    class ItemInfo
    {
        [JsonProperty("classid")]
        public string Classid { get; set; }
        [JsonProperty("instanceid")]
        public string Instanceid { get; set; }
        [JsonProperty("our_market_instanceid")]
        public string Our_market_instanceid { get; set; }
        [JsonProperty("market_name")]
        public string Market_name { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("market_hash_name")]
        public string Market_hash_name { get; set; }
        [JsonProperty("rarity")]
        public string Rarity { get; set; }
        [JsonProperty("quality")]
        public string Quality { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("mtype")]
        public string Mtype { get; set; }
        [JsonProperty("slot")]
        public string Slot { get; set; }
        [JsonProperty("stickers")]
        public string Stickers { get; set; }
        [JsonProperty("description")]
        public description[] Description { get; set; }
        [JsonProperty("tags")]
        public tags[] Tags { get; set; }
        [JsonProperty("hash")]
        public string Hash { get; set; }
        [JsonProperty("min_price")]
        public string Min_price { get; set; }
        [JsonProperty("offers")]
        public offers[] Offers { get; set; }
        [JsonProperty("buy_offers")]
        public buy_offers[] Buy_offers { get; set; }
    }
    public class description
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
    }
    public class tags
    {
        [JsonProperty("CSGO_Type_WeaponCase")]
        public string Internal_name { get; set; }
        [JsonProperty("Контейнер")]
        public string Name { get; set; }
        [JsonProperty("Type")]
        public string Category { get; set; }
        [JsonProperty("category_name")]
        public string Category_name { get; set; }
    }
    public class offers
    {
        [JsonProperty("price")]
        public string Price { get; set; }
        [JsonProperty("count")]
        public string Count { get; set; }
        [JsonProperty("my_count")]
        public string My_count { get; set; }
    }

    public class buy_offers
    {
        [JsonProperty("c")]
        public string C { get; set; }
        [JsonProperty("my_count")]
        public string My_count { get; set; }
        [JsonProperty("o_price")]
        public string O_price { get; set; }
    }
}
