using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using CurrencyTransfere2.Models;

namespace CurrencyTransfere2.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<CurrencyService> _logger;
        public CurrencyService(HttpClient httpClient, ILogger<CurrencyService> logger )
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<CurrencyModel?> GetCurrenciessAsync()//metoda ktera ziska pomoci Injectnuteho HTTPClienta data z URI
        {
            try
            {
                CurrencyModel? data = await _httpClient.GetFromJsonAsync<CurrencyModel>(
                    $"https://data.kurzy.cz/json/meny/b[1].json",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web )
                    {
                        PropertyNameCaseInsensitive = false
                    });

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting something fun to say: {Error}", ex);
            }

            return null;
        }
    }
}
