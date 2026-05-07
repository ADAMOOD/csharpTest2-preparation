using System.Text.Json;
using c_Test2_preparatio_currency_.Models;

namespace c_Test2_preparatio_currency_.Service
{
    public class CurrencyService
    {
        private readonly HttpClient _client;
        private ILogger<CurrencyService> logger;
        public CurrencyService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
        }
        public async Task<CurrencyResponse?> GetCurrencyAsync()
        {
            try
            {
                CurrencyResponse? currs = await _client.GetFromJsonAsync<CurrencyResponse>(
                    $"https://data.kurzy.cz/json/meny/b[1].json",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return currs ;
            }
            catch (Exception ex)
            {
                logger.LogError("Error getting something fun to say: {Error}", ex);
            }

            return null;
        }
    }
}
