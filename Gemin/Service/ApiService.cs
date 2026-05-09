using Microsoft.AspNetCore.Server.HttpSys;
using System.Net;
using System.Resources;
using System.Text.Json.Nodes;

namespace Gemin.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ValidateDICAsync(string DIC)
        {
            bool first = true;
            var response = new HttpResponseMessage();

            while (first || response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                first = false;

                Dictionary<string, string> pair = new Dictionary<string, string>();
                pair.Add("dic", DIC);

                HttpRequestMessage request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://csharp.janjanousek.cz/api/dic/"),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(pair) //tady je nutno poslat to encoded jinak to nebude fungovat
                };
                request.Headers.Add("X-AppName", "vsb");

                response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    break;
                }
                else if (response.StatusCode != HttpStatusCode.TooManyRequests && response.StatusCode != HttpStatusCode.InternalServerError)
                {
                    throw new Exception("error occured while comunicating with api");
                    
                }

                // Nejdřív zkontrolujeme, jestli vůbec existuje hlavička RetryAfter, a pak jestli má Deltu
                if (response.Headers.RetryAfter != null && response.Headers.RetryAfter.Delta.HasValue)
                {
                    // Tady MUSÍ být await, jinak to nečeká
                    await Task.Delay(response.Headers.RetryAfter.Delta.Value);
                }
                else
                {
                    // Tady taky MUSÍ být await
                    await Task.Delay(2000); // Počkáme 2 sekundy pro jistotu
                }

            }
            var json = await response.Content.ReadAsStringAsync();
            // JSON se jen "načte" a rozebere do paměti (nepřekládá se do tvé třídy)
            JsonNode jsonNode = JsonNode.Parse(json);// tahle node je proste pure data z jsonu
            return jsonNode["valid"].GetValue<bool>();// api vraci vlastnost "valid"
        }
    }
}
