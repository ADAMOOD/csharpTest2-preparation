using CitiesWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CitiesWPF.Services
{
    class ApiService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public ApiService() { }

        public async Task<List<Subject>?> sendRequestAsync(string city)
        {
            var cityLower = city.ToLower();
            string url = $"https://dataor.justice.cz/api/file/zp-full-{cityLower}-2024.xml";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var xml = await response.Content.ReadAsStringAsync();
                XDocument document = XDocument.Parse(xml);
                List<Subject> subjects = new List<Subject>();
                foreach (XElement subject in document.Descendants("Subjekt"))
                {
                    var nazev = subject.Descendants("nazev").FirstOrDefault();
                    var ico = subject.Descendants("ico").FirstOrDefault();
                    var datum = subject.Descendants("zapisDatum").FirstOrDefault();
                    subjects.Add(new Subject()
                    {
                        Nazev = nazev.Value,
                        ico = ico.Value,
                        zapisDatum = datum.Value

                    });
                }
                if (subjects.Any())
                {
                    return subjects;
                }
            }
            return null;
        }
    }
}
