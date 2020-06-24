namespace BotCsgo.Helpers.Parser.Steam
{
    class SteamPriceSettings : IParserSettings
    {
        public SteamPriceSettings(string search)
        {
            Search = search;
        }

        public string MainUrl { get; set; } = "https://steamcommunity.com";
        public int Page { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public string Search { get; set; }
        public int Stickers { get; set; }
        public string Type { get; set; }
        public string GetPrefix()
        {
            return $"market/search?q={Search}";
        }
    }
}
