namespace FinancialOfice.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetOfficeResponse()
        {
           return await _httpClient.GetAsync("https://apl2.czso.cz/iSMS/do_cis_export?kodcis=46&typdat=0&cisjaz=203&format=0");
        }
    }
}
