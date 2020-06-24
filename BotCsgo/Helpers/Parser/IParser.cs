using HtmlAgilityPack;

namespace BotCsgo.Helpers.Parser
{
    public interface IParser<T> where T : class
    {
        T ParserData(HtmlDocument document);
    }
}
