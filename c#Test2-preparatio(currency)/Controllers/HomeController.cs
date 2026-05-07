using System.Diagnostics;
using c_Test2_preparatio_currency_.Models;
using c_Test2_preparatio_currency_.Service;
using Microsoft.AspNetCore.Mvc;

namespace c_Test2_preparatio_currency_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CurrencyService _service;

        public HomeController(ILogger<HomeController> logger, CurrencyService service)
        {
            _logger = logger;
            _service=service;
        }

        public async  Task<IActionResult> Index()
        {
            var currs = await _service.GetCurrencyAsync();
            return View(currs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Index(double castka, string currency)
        {
            var currs = await _service.GetCurrencyAsync();
            if (!String.IsNullOrEmpty(currency))
            {
                var conversion = currs.Kurzy[currency];
                ViewBag.Vysledek = castka * conversion.dev_stred;
                ViewBag.Currency = currency;
            }
            return View(currs);
        }
    }
}
