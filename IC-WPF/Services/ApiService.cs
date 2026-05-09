using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IC_WPF
{
    class ApiService
    {
        private readonly HttpClient _httpClient;
        private string Uri = "https://ares.gov.cz/ekonomicke-subjekty-v-be/rest/ekonomicke-subjekty/";
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> makeRequest(string IC)
        {
            Uri += IC;
           return await _httpClient.GetAsync(Uri);
        }
    }
}
