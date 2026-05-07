using Menza_XML.Models;
using Menza_XML.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Menza_XML.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiService _apiService;

        public HomeController(ILogger<HomeController> logger,ApiService api)
        {
            _logger = logger;
            _apiService = api;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(DateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return RedirectToAction("Food", new { date = model.Date.ToString("yyyy-MM-dd") });
        }
        public async Task<IActionResult> Food(DateViewModel model)
        {
            Stream xmlStream = await _apiService.makeRequestAsync(model.Date);
            XmlSerializer serializer = new XmlSerializer(typeof(FoodModel));
            List<FoodModel> foods = (List<FoodModel>) serializer.Deserialize(xmlStream);
            return View(foods);
        }
    }

}
