using BotCsgo.Model.Response;
using HtmlAgilityPack;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BotCsgo.Helpers.Parser
{
    public class ParserWorker<T> where T: class
    { 
        private IParserSettings parserSettings;
        public IParserSettings Settings
        {
            get { return parserSettings; }
            set { parserSettings = value; }
                
        }
        private IParser<T> parser;
        public event Action<object, T> OnNewData;
        public event Action<object> OnComplated;
        public bool IsWorked = true;
        public ParserWorker(IParserSettings parserSettings, IParser<T> parser)
        {
            Settings = parserSettings;
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }

        public async void Start()
        {
            HtmlLoader htmlLoader = new HtmlLoader(Settings);
            var html = await htmlLoader.GetSoursePage();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var countPage = htmlDocument.DocumentNode.Descendants("span").Where(count => count.GetAttributeValue("id", "").Equals("total_pages")).FirstOrDefault().InnerText;
            for (int i = parserSettings.Page; i < Convert.ToInt32(countPage); i++)
            {
                if (!IsWorked)
                {
                    OnComplated?.Invoke(this);
                    return;
                }
                html = await htmlLoader.GetSoursePage();
                htmlDocument.LoadHtml(html);
                var items = parser.ParserData(htmlDocument);
                OnNewData?.Invoke(this, items);
            }
            OnComplated?.Invoke(this);
            IsWorked = false;
        }
        public async void StartOnce()
        {
            HtmlLoader htmlLoader = new HtmlLoader(Settings);
            var html = await htmlLoader.GetSoursePage();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var result = parser.ParserData(htmlDocument);
            OnNewData?.Invoke(this, result);
        }
        public void Stop()
        {
            IsWorked = false;
        }
    }
}
