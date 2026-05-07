using CurrencyTransfere2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using CurrencyTransfere2.Services;

namespace CurrencyTransfere2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CurrencyService _currencyService;
        private readonly DatabaseService _databaseService;

        public HomeController(ILogger<HomeController> logger, CurrencyService service, DatabaseService databaseService)
        {
            _logger = logger;
            _currencyService = service;
            _databaseService = databaseService;
        }

        public async Task<IActionResult> Index()
        {
            var response =await _currencyService.GetCurrenciessAsync();
            return View(new ExchangeViewModel
            {
                Currency = response
            });
        }
        [HttpPost]
        public async Task<IActionResult> Index(ExchangeViewModel model)
        {
            model.Currency = await _currencyService.GetCurrenciessAsync();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Result = model.Ammount * model.Currency.Currency[model.SelectedCurrency].dev_stred;
            Exchange e = new Exchange()
            {
                Name = model.Name,
                Email = model.Email,
                Ammount = model.Ammount,
                SelectedCurrency = model.SelectedCurrency,
                Result = model.Result
            };
            await _databaseService.InsertExchangeAsync(e);
            return RedirectToAction("History");//pokud vse okay presmerujeme uzivatelena history
        }

        public async Task<IActionResult> History()
        {
            var exchanges = await _databaseService.GetAllExchangesAsync();
            return View(exchanges);
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
    }
}
