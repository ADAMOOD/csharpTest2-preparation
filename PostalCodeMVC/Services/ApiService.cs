namespace PostalCodeMVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> SendRequestAsync(string PostalCode)
        {
            HttpResponseMessage responseMessage = null;
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://csharp.janjanousek.cz/api/osm-xml/");//vytvoreni requestu s Metodou POST a URI
            requestMessage.Headers.Add("Authorization", "Bearer VSB");//poslani Authentication Bearer VSB
            string json = $"{{\"postalcode\":\"{PostalCode}\"}}";//vytvoreni postalcode json
            StringContent content = new StringContent(json,System.Text.Encoding.UTF8,"application/json");//zaobaleni jsonu a zakodovani do content objektu
            requestMessage.Content = content;

          return await _httpClient.SendAsync(requestMessage);
        }
    }
}
