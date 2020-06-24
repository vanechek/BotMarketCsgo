using HtmlAgilityPack;
using System.Linq;
using System.Windows;

namespace BotCsgo.Helpers.Parser.Steam
{
    class SteamNameParser : IParser<string>
    {
        public string ParserData(HtmlDocument document)
        {
            var html = document.DocumentNode.Descendants("div").Where(div => div.GetAttributeValue("div", "").Equals("market_listing_item_name_block")).FirstOrDefault();
            return html.Descendants("span").Where(@class => @class.GetAttributeValue("class", "").Equals("market_listing_item_name")).FirstOrDefault().InnerText;
        }
    }
}
