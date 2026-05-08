using Menza_XML.Models;
using Menza_XML.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Xml.Serialization;

namespace Menza_XML.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiService _apiService;
        private readonly DbService _dbService;

        public HomeController(ILogger<HomeController> logger, ApiService api, DbService dbService)
        {
            _logger = logger;
            _apiService = api;
            _dbService = dbService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _dbService.GetAllOrders();
            
            return View(new IndexViewModel()
            {
                Orders = orders
            });
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return RedirectToAction("Food", new { date = model.Date.ToString("yyyy-MM-dd") });
        }
        public async Task<IActionResult> Food(IndexViewModel model)
        {
            string xmlString = await _apiService.makeRequestAsync(model.Date);
            XmlSerializer serializer = new XmlSerializer(typeof(FoodModel));
            var foods = DeserializeFromString<MenzaResponse>(xmlString);
            ViewBag.date = model.Date;
            return View(foods);
        }

        public IActionResult Detail(int altId, string name, string date)
        {
            ModelState.Clear();

            return View(new FoodDbViewModel()
            {
                Date = date,
                Food = new FoodModel { altId = altId, name = name }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Detail(FoodDbViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var record = new dbRecordModel()
            {
                FoodAltId = model.Food.altId,
                UserName = model.Name,
                FoodName = model.Food.name,
                OrderDate = model.Date
            };
            await _dbService.saveAsync(record);
            return RedirectToAction("Index");
        }
        public static T DeserializeFromString<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                return (T)serializer.Deserialize(stream);
            }
        }
    }

}
