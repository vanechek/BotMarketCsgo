namespace BotCsgo.Helpers.Parser
{
    public interface IParserSettings
    {
        string MainUrl { get; set; }
        string GetPrefix();
        int Page { get; set; }
        int MinPrice { get; set; }
        int MaxPrice { get; set; }
        string Search { get; set; }
        int Stickers { get; set; }
        string Type { get; set; }
    }
}
