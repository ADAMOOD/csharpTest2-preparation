using Microsoft.AspNetCore.Mvc;
[Route("api/menza-xml")]
public class MenzaMockController : ControllerBase
{
    [HttpPost]
    public IActionResult GetMenu([FromForm] string date)
    {
        // 1. Kontrola hlavičky (Bod 3 zadání)
        var auth = Request.Headers["Authorization"].ToString();
        if (auth != "Bearer VSB") return Unauthorized("Chybí Bearer VSB");

        // 2. Simulace náhodné chyby 429 (Bod 3 zadání)
        if (new Random().Next(1, 4) == 1) // 33% šance na chybu
        {
            Response.Headers.Append("Retry-After", "5"); // Počkej 5 sekund
            return StatusCode(429, "Too Many Requests");
        }

        // 3. Vrácení XML (Bod 3 a 4 zadání)
        string xml = $@"<MenzaResponse>
            <item><name>Svíčková</name><price>120</price><mealKindId>2</mealKindId><altId>101</altId></item>
            <item><name>Pivo</name><price>45</price><mealKindId>1</mealKindId><altId>102</altId></item>
            <item><name>Salát</name><price>80</price><mealKindId>2</mealKindId><altId>103</altId></item>
        </MenzaResponse>";

        return Content(xml, "application/xml");
    }
}