using System.Net;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.JavaScript;

namespace Menza_XML.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<String> makeRequestAsync(DateTime date)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            bool first = true;
            while (first || response.StatusCode == HttpStatusCode.TooManyRequests )
            {
                first = false;

                Dictionary<string, string> dateDictionary = new Dictionary<string, string>();
                dateDictionary.Add("date", date.ToString("yyyy-MM-dd"));

                HttpRequestMessage message = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://localhost:7183/api/menza-xml"),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(dateDictionary)//tady je nutno poslat to encoded jinak to nebude fungovat
                };
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "VSB");//pridame do hlavicky schema a klic
                response = await _httpClient.SendAsync(message);


                if (response.IsSuccessStatusCode)
                {
                    break;
                }
                if (response.StatusCode != HttpStatusCode.TooManyRequests)
                {
                    throw new Exception("Nastala neocekavana chyba pri requestu na api");
                }
                if (response.Headers.RetryAfter != null && response.Headers.RetryAfter.Delta.HasValue)
                {
                    await Task.Delay(response.Headers.RetryAfter.Delta.Value);//pockej cas ktery je v hlavicce
                }
                else
                {
                    await Task.Delay(new TimeSpan(0, 0, 0, 2));//pockej pro jistotu 2 sekundy
                }
            }

            return await response.Content.ReadAsStringAsync();
        }

    }
}
