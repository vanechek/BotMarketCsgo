using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BotCsgo.Helpers
{
    public class BaseGetMarketApi<NameObjectForDeserialize> where NameObjectForDeserialize : new()
    {
        public static async void GetMarketAsync(string get, string your_secret_key)
        {
            HttpClient client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(your_secret_key))
            {
                string responseBody = "";
                try
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    var response = await client.GetAsync(get).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode == true)
                    {
                        responseBody = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static async Task<NameObjectForDeserialize> GetMarketAsyncWithData(string get, string your_secret_key)
        {
            NameObjectForDeserialize objectForDeserialize;
            HttpClient client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(your_secret_key))
            {
                string responseBody = "";
                try
                { 
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    var response = await client.GetAsync(get).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode == true)
                    {
                        responseBody = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                objectForDeserialize = JsonConvert.DeserializeObject<NameObjectForDeserialize>(responseBody);  
            }
            else
            {
                return objectForDeserialize = default(NameObjectForDeserialize);
            }
            return objectForDeserialize;
        }
    }
}
