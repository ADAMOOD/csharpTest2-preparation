using FinancialOfice.Models;
using FinancialOfice.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;

namespace FinancialOfice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiService _apiService;

        public HomeController(ILogger<HomeController> logger, ApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _apiService.GetOfficeResponse();
            if (response is null)
            {
                return View();
            }
            var xml = await response.Content.ReadAsStringAsync();
            XDocument document = XDocument.Parse(xml);
            var polozky = document.Descendants("POLOZKA");
            List<Office> offices = new List<Office>();
            foreach (var polozka in polozky)
            {
                offices.Add(new Office()
                {
                    Chodnota = polozka.Element("CHODNOTA")?.Value,
                    Name = polozka.Element("TEXT")?.Value
                });
            }
            return View(offices);
        }

        public async Task<IActionResult> Detail(string chodnota)
        {
            FormViewModel fvm = new FormViewModel()
            {
                Chodnota = chodnota
            };
            return View(fvm);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(FormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if ( await DataStorage.SaveObjectToFile("model.txt", model))
            {
                TempData["SuccessMessage"] = "Formuláø byl úspìšnì uložen do souboru!";
            }
            else
            {
                TempData["ErrorMessage"] = "Chyba pri zapisovani do souboru!";
            }

            return RedirectToAction("Index");
        }
    }
}
