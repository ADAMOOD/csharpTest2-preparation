using System.Net;

namespace FinalGeminiBossFight.Services
{
    public class CurrencyApiService
    {
        private readonly HttpClient _httpClient;
        public CurrencyApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> GetAllCurrencies()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            bool first = true;
            while (first || response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                first = false;
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://csharp.janjanousek.cz/api/cnb/?date=2025-05-12");
                requestMessage.Headers.Add("x-api-key", "VSB");
                response = await _httpClient.SendAsync(requestMessage);
                if(response.IsSuccessStatusCode)
                {
                    break;
                }
                if (response.StatusCode == HttpStatusCode.TooManyRequests) {
                    if(response.Headers.RetryAfter is not null && response.Headers.RetryAfter.Delta.HasValue)
                    {
                        Task.Delay(response.Headers.RetryAfter.Delta.Value);//cekame podle casu ktery se nam vratil v response
                    }
                    else
                    {
                        Task.Delay(2000);//pockame
                    }
                }
            }
            return response;
        }
        public async Task<HttpResponseMessage> CheckBakCode(string bankCode)
        {
            HttpRequestMessage reqquest = new HttpRequestMessage(HttpMethod.Post, "https://csharp.janjanousek.cz/api/osm-xml/");
            reqquest.Headers.Add("Authorization", "Bearer VSB");
            string code = bankCode.Substring(bankCode.IndexOf('/')+1);//posunout se vedle lomitka
            string json = $"{{\"postalcode\":\"{code}\"}}";
            StringContent jsonEncoded = new StringContent(json,System.Text.Encoding.UTF8,"application/json");// na tohle jsem zapomnel ze je to StringContent a ne HttpContent (musel jsem se podivat)
            reqquest.Content = jsonEncoded;
            return await _httpClient.SendAsync(reqquest);
        }
    }
}
