using System.Net.Http;
using System.Threading.Tasks;

namespace BotCsgo.Helpers.Parser
{
    class HtmlLoader
    {
        private HttpClient httpClient;
        private string url;

        public HtmlLoader(IParserSettings parserSettings)
        {
            url = $"{parserSettings.MainUrl}/{parserSettings.GetPrefix()}";
            httpClient = new HttpClient();
        }
        public async Task<string> GetSoursePage()
        {
            string responseBody = "";
            var response = httpClient.GetAsync(url).Result;
            if (response.IsSuccessStatusCode == true)
            {
                responseBody = await response.Content.ReadAsStringAsync();
            }
            return responseBody;
        }
    }
}
