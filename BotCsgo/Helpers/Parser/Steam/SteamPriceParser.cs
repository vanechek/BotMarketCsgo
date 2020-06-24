using HtmlAgilityPack;
using System.Linq;

namespace BotCsgo.Helpers.Parser
{
    public class SteamPriceParser : IParser<string>
    {
        public string ParserData(HtmlDocument document)
        {
            string price;
            try
            {
                var html = document.DocumentNode.Descendants("span").Where(price => price.GetAttributeValue("class", "").Equals("market_table_value normal_price")).FirstOrDefault();
                price = html.Descendants("span").Where(@class => @class.GetAttributeValue("class", "").Equals("sale_price")).FirstOrDefault().InnerText;
            }
            catch
            {
                return "";
            }
            return price;
        }
    }
}
