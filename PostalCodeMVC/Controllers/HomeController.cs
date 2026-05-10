using Microsoft.AspNetCore.Mvc;
using PostalCodeMVC.Models;
using PostalCodeMVC.Services;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PostalCodeMVC.Controllers
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

        public IActionResult Index()
        {
            return View(new IndexModelView());
        }

        [HttpPost]
        public IActionResult Index(IndexModelView model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return RedirectToAction("Form", new { PostalCode = model.PostalCode });
        }
        public async Task<IActionResult> Form(string PostalCode)
        {
            var response = await _apiService.SendRequestAsync(PostalCode);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // 1. Chybu p¯id·v·me do ModelState. PrvnÌ parametr je p¯esn˝ n·zev vlastnosti!
                ModelState.AddModelError("PostalCode", "Toto PS» neexistuje v API.");

                // 2. Vytvo¯Ìme model a vloûÌme do nÏj to, co uûivatel zadal, aù to tam m· p¯edvyplnÏnÈ
                IndexModelView model = new IndexModelView { PostalCode = PostalCode };

                // 3. Vr·tÌme pohled "Index" a p¯ed·me mu model s chybami. (é·dn˝ redirect!)
                return View("Index", model);

            }
            var xml = await response.Content.ReadAsStringAsync();
            XDocument document = XDocument.Parse(xml);
            var x = document.Descendants("county").FirstOrDefault();

            FormViewModel formViewModel = new FormViewModel()
            {
                PostalCode = PostalCode,
                Country = x.Value
            };
            return View(formViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Form(FormViewModel formViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(formViewModel);
            }
            FileStream fileStream = new FileStream($"{formViewModel.PostalCode}{formViewModel.PhoneNumber.Remove(0, 1)}.txt", FileMode.Create);
            await FileService.WriteObjectToFilePostalCode(fileStream, formViewModel);

            TempData["success"] = $"Uspesne ulozeno do {fileStream.Name}";
            return RedirectToAction("Index");
        }

    }
}
