# Programování v C# II – test

*Během testu je možné používat materiály, které na doméně: https://docs.microsoft.com/. Žádné jiné materiály povoleny nejsou!*

## Zadání

Vytvořte webovou aplikaci (MVC), která bude splňovat následující:

1. Na úvodní stránce bude zobrazen seznam firem vytažený z předpřipravené databáze (`company.db`). U každé položky v seznamu (např. v tabulce) bude odkaz nebo tlačítko pro **vymazání dané firmy z databáze**. Na této stránce bude také jasně viditelný odkaz pro přechod na formulář přidání nové firmy.

2. Na druhé stránce bude formulář pro zadání následujících údajů: 
   * **Název firmy** (text)
   * **DIČ** (text)
   * **Počet zaměstnanců** (celé číslo, minimálně 1)
   * **Právní forma** (výběr z roletky pomocí Enum – např. s.r.o., a.s., OSVČ)
   * **Poznámka** (víceřádkový text, maximálně 200 znaků)
   
   Všechna pole kromě poznámky budou povinná (data řádně validujte přes anotace v modelu).

3. Po odeslání validního formuláře dojde k ověření DIČ přes externí API službu. Požadavek na API pošlete pomocí `HttpClient` na URL: `https://localhost:7183/api/dic-verify` (přizpůsobte si port dle vašeho lokálního serveru).
   * Požadavek musí být odeslán metodou **POST**.
   * V těle požadavku bude předáno zadané DIČ v **URL-encoded** formátu s klíčem `dic`.
   * Požadavek musí obsahovat hlavičku `ApiKey` s hodnotou `VSB-Test`.

4. API vrací data ve formátu JSON. Služba je ovšem nestabilní a může vrátit stavový kód **500 (Internal Server Error)**. V takovém případě musí vaše aplikace **počkat přesně 5 sekund** a pokusit se požadavek odeslat znovu (ověřte, že neblokujete hlavní vlákno aplikace pomocí `Thread.Sleep`, ale využijete asynchronní čekání).

5. Ze získaného JSONu nás bude zajímat pouze vlastnost `isValid` (boolean). 
   * Pokud je hodnota `true`, uložíte všechna data z formuláře do SQLite databáze do tabulky `Company` a přesměrujete uživatele zpět na úvodní stránku se seznamem.
   * Pokud je `false`, uložení neproběhne a uživateli se znovu zobrazí formulář s chybovou hláškou (např. přes `ModelState`), že zadané DIČ je neplatné.

6. Pro práci s databází použijte balíčky `Microsoft.Data.Sqlite.Core` a `SQLitePCLRaw.bundle_green` (případně můžete využít knihovnu `Dapper`). Connection string pro databázi je `Data Source=company.db` (ujistěte se, že máte správnou cestu k souboru s databází). V databázi si tabulku `Company` musíte zinicializovat sami, pokud neexistuje.

---

## Příloha: Mock API pro lokální testování

Pro simulaci chování reálného školního API si do projektu přidejte následující kontroler. Zajistí vám simulaci výpadků (chyba 500) i ověření hlavičky a formátu:

```csharp
using Microsoft.AspNetCore.Mvc;
using System;

namespace VsbTestApp.Controllers
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