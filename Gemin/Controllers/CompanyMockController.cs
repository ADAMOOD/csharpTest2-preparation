using Microsoft.AspNetCore.Mvc;

namespace Gemin.Controllers
{

    [Route("api/dic-verify")]
    [ApiController]
    public class CompanyMockController : ControllerBase
    {
        [HttpPost]
        public IActionResult VerifyDic([FromForm] string dic)
        {
            // 1. Kontrola požadované hlavičky
            var apiKey = Request.Headers["ApiKey"].ToString();
            if (apiKey != "VSB-Test")
            {
                return Unauthorized(new { error = "Chybí nebo je neplatný ApiKey." });
            }

            // 2. Simulace pádu serveru (chyba 500) - cca 33% šance
            if (new Random().Next(1, 4) == 1)
            {
                return StatusCode(500, new { error = "Internal Server Error - Zkuste to znovu." });
            }

            // 3. Simulace logiky ověření DIČ (validní musí začínat na 'CZ')
            bool isDicValid = !string.IsNullOrEmpty(dic) && dic.StartsWith("CZ");

            // 4. Sestavení a vrácení JSON odpovědi
            var response = new
            {
                dic = dic,
                isValid = isDicValid,
                checkedAt = DateTime.Now
            };

            return Ok(response);
        }
    }
}
