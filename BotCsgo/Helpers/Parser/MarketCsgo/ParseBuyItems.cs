using BotCsgo.Model.Response;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BotCsgo.Helpers.Parser
{
    public class ParserBuyItems : IParser<ObservableCollection<items>>
    {
        private string ClassId;
        private string InstanceId;
        private string title;
        private string src;
        private List<HtmlNode> ManeCollection { get; set; }
        public ObservableCollection<items> ParserData(HtmlDocument document)
        {
            ObservableCollection<items> items = new ObservableCollection<items>();
            var itemsHtml = document.DocumentNode.Descendants("div")
                .Where(items => items.GetAttributeValue("class", "")
                .Equals("market-items")).ToList();
            var itemsList = itemsHtml[0].Descendants("a")
    .Where(items => items.GetAttributeValue("class", "")
    .Equals("item")).ToList();
            ManeCollection = itemsList;
            var itemshotList = itemsHtml[0].Descendants("a")
                .Where(items => items.GetAttributeValue("class", "")
                .Equals("item hot")).ToList();
            AddManeCollection(itemshotList);
            var itemsaList = itemsHtml[0].Descendants("a")
                    .Where(items => items.GetAttributeValue("class", "")
                    .Equals("item ")).ToList();
            AddManeCollection(itemsaList);
            foreach (var item in itemsList)
            {
                var href = item.GetAttributeValue("href", "");
                GetClassidAndInstanseId(href);
                var stickerList = item.Descendants("img").ToList();
                ObservableCollection<Sticker> stickers = new ObservableCollection<Sticker>();
                if (stickerList.Count > 0)
                {
                    foreach (var stickerHtml in stickerList)
                    {
                        if (!string.IsNullOrWhiteSpace(stickerHtml.GetAttributeValue("src", "")))
                        {
                            Sticker sticker1 = new Sticker()
                            {
                                Title = stickerHtml.GetAttributeValue("title", "").Replace("Наклейка: ", "").Replace("Patch: ", "").Replace("Нашивка: ", ""),
                                 Path= stickerHtml.GetAttributeValue("src", "")
                            };
                            stickers.Add(sticker1);
                        }
                    }
                }
                items currentItem = new items()
                {
                    Market_hash_name = item.Descendants("div")
                    .Where(name => name.GetAttributeValue("class", "").Equals("name"))
                    .FirstOrDefault().InnerText.Trim('\r', '\n', '\t').Replace("\n", "").Replace(" ", ""),
                    Price = item.Descendants("div")
                    .Where(price => price.GetAttributeValue("class", "").Equals("price"))
                    .FirstOrDefault().InnerText.Trim('\r', '\n', '\t').Replace(" ", ""),
                    Classid = ClassId,
                    Instanceid = InstanceId,
                    Path = item.Descendants("div")
                    .Where(nameClass => nameClass.GetAttributeValue("class", "").Equals("image"))
                    .FirstOrDefault().GetAttributeValue("style", "")
                    .Replace("background-image: url(", "").Replace(");", ""),
                    urlItem = $"https://market.csgo.com{href}",
                    Stickers = stickers,
                };

               
                items.Add(currentItem);
                ClassId = "";
                InstanceId = "";
            }
            return items;
        }
        public string GetCountPage(HtmlDocument document)
        {
            var htmlNode = document.DocumentNode.Descendants("span").Where(count => count.GetAttributeValue("id", "").Equals("total_pages")).FirstOrDefault().InnerText;
            return htmlNode;
        }
        private void AddManeCollection(List<HtmlNode> htmlNode)
        {
            foreach (var item in htmlNode)
            {
                ManeCollection.Add(item);
            }
        }

        private void GetClassidAndInstanseId(string href)
        {
            this.title = "";
            this.src = "";
            var newhref = href.Substring(6);
            char symbola = char.Parse("-");
            int countDefis = 0;
            for (int i = 0; i < newhref.Length; i++)
            {
                char symbol = char.Parse(newhref[i].ToString());
                if (symbol == symbola && countDefis <= 1)
                {
                    countDefis++;
                    continue;
                }
                if (countDefis == 0)
                {
                    ClassId += symbol;
                }
                else
                {
                    if (countDefis == 2)
                    {
                        break;
                    }
                    InstanceId += symbol;
                }
            }
        }
    }
}
