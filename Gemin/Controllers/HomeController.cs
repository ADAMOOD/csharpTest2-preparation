using Gemin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gemin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> NewCompany()
        {
            return View();
        }
    }
}
