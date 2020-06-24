namespace BotCsgo.Helpers.Parser
{
    class BuyItemsSettings : IParserSettings
    {
        public BuyItemsSettings(int minPrice, int maxPrice, string search, int page, int sticker)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            Search = search;
            Page = page;
            Stickers = sticker;
        }

        public string MainUrl { get; set; } = @"https://market.csgo.com";
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public string Search { get; set; }
        public int Stickers { get; set; }
        public string Type { get; set; }
        public int Page { get; set; }
        public string GetPrefix()
        {
            return $"?r=&q=&p={Page}&rs={MinPrice};{MaxPrice}&search={Search}&h=&fst={Stickers}";
        }
    }
}
