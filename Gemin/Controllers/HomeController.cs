using Gemin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Gemin.Service;

namespace Gemin.Controllers
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
            var companies = await _dbService.GetAllCompanmiesAsync();
            return View(companies);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _dbService.DeleteCompany(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> NewCompany(Company companyModel)
        {
            if (!ModelState.IsValid) //kontrola validace
            {
                return View(companyModel);
            }
            bool valid = await _apiService.ValidateDICAsync(companyModel.DIC);
            if (!valid)
            {
                ModelState.AddModelError("DIC", "Zadané DIÈ není platné v systému ARES.");
                return View(companyModel);
            }

            await _dbService.InsertCompanyAsync(companyModel);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> NewCompany()
        {
            return View();
        }
    }
}
