using FinalGeminiBossFight.Models;
using FinalGeminiBossFight.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace FinalGeminiBossFight.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CurrencyApiService _currencyApiService;
        private readonly DbService _dbService;

        public HomeController(ILogger<HomeController> logger, CurrencyApiService currencyApiService, DbService dbService)
        {
            _logger = logger;
            _currencyApiService = currencyApiService;
            _dbService = dbService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _currencyApiService.GetAllCurrencies();
            var json = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = false;
            IndexViewModel indexViewModel = JsonSerializer.Deserialize<IndexViewModel>(json,options);
            
            return View(indexViewModel);
        }

        //po loadnutni stranky
        public async Task<IActionResult> Form(string code)
        {
            
            return View(new FormViewModel() { CurrencyCode = code});
        }

        //po kliknuti na odeslani formulare
        [HttpPost]
        public async Task<IActionResult> Form(FormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var response = await _currencyApiService.CheckBakCode(model.AccountNumber);
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ModelState.AddModelError("Account", $"Account {model.AccountNumber} not found in api");
                return View(model);
            }
            TempData["info"] = "Chyba pri nacitani dat z API PRO OVERENI UCTU";
            if (response.IsSuccessStatusCode)
            {
                string xml = await response.Content.ReadAsStringAsync();
                XDocument document = XDocument.Parse(xml);
                var county = document.Descendants("county").FirstOrDefault();//tady musi byt first or default 
                Transfer newRecord = new Transfer()
                {
                    Name=model.Name,
                    Email=model.Email,
                    Ammount = model.Ammount,
                    CurrencyCode = model.CurrencyCode,
                    AccountNumber = model.AccountNumber,
                    County = county.Value
                };

                int? id = await _dbService.InsertTransfareAsync(newRecord);
                if(id is not null)
                {
                    TempData["info"] = "Uspesne ulozeno do DATABAZE";
                }
                else
                {
                    TempData["info"] = "Chyba pri ukladani do DATABAZE";
                }
                string fileName = $"{newRecord.Name}_{newRecord.CurrencyCode}.txt";
                using(FileStream fs = new FileStream(fileName,FileMode.Create))
                {
                    FileService.WriteObjectIntoFile(fs, newRecord);
                }
                TempData["info"] += $"\r\n Taktez uspesne zapsano do souboru {fileName}";
            }
            return RedirectToAction("Index");
        }


    }
}
