# Programování v C# II – test (Ultimátní MVC výzva)

*Během testu je možné používat materiály, které jsou na doméně: https://learn.microsoft.com/. Žádné jiné materiály povoleny nejsou!*

## Zadání

Vytvořte webovou aplikaci pomocí **ASP.NET Core MVC**, která bude splňovat níže uvedené požadavky:

1. Uživatelské rozhraní bude v češtině/slovenštině a bude uživatelsky přívětivé. Veškeré validační hlášky budou konkrétní a budou uvedeny u formulářových prvků, kterých se týkají.

2. **Úvodní obrazovka** bude obsahovat seznam dostupných měn a jejich kurzů (vůči CZK). Data pro tento seznam získáte z API na adrese:
   `https://csharp.janjanousek.cz/api/cnb/?date=2025-05-12`
   * Při komunikaci s API je nutné předat API klíč v HTTP hlavičce `x-api-key`. Klíč pro komunikaci je `VSB`.
   * API vrací JSON dokument. Z dat vás zajímá pouze kód měny (např. EUR, USD) a její kurz.
   * *Pozor! API může náhodně vrátit HTTP stavový kód "429 Too Many Requests". V takovém případě je vrácena hlavička "Retry-After", která obsahuje čas v sekundách, po kterém se má dotaz opakovat. Zařiďte, aby aplikace daný čas skutečně počkala a následně požadavek opakovala.*

3. U každé vypsané měny na úvodní obrazovce bude odkaz (tlačítko) "Vytvořit žádost". Po kliknutí na tento odkaz dojde k přesměrování na další stránku (formulář), kde bude **v rámci URL předán kód vybrané měny**.

4. **Druhá stránka** bude obsahovat formulář pro zadání **Jména příjemce**, **E-mailu**, **Částky** (desetinné číslo) a **Čísla účtu**. Kód měny bude do formuláře skrytě předán z URL. Všechna pole budou povinná.
   * E-mail musí mít platný formát.
   * Číslo účtu musí mít správný formát (1-6 číslic následovaných pomlčkou, za kterou budou 2 až 10 čísel. Následovat musí lomítko a další 4 čísla). Například: `4568-42494644/0300`. Validujte pomocí regulárního výrazu.

5. Po odeslání validního formuláře dojde k ověření kódu banky (poslední 4 čísla za lomítkem) proti dalšímu API, které je na adrese:
   `https://csharp.janjanousek.cz/api/osm-xml/`
   * *Pozor! URL musí být včetně lomítka na konci.*
   * Při komunikaci s API je nutné předat HTTP hlavičku `Authorization`. Hodnota se bude skládat ze schématu `Bearer` a hodnoty `VSB`. Výsledná hodnota hlavičky `Authorization` tedy bude `Bearer VSB`.
   * Požadavek na API musí být proveden HTTP metodou **POST**.
   * Tělo požadavku musí obsahovat JSON (*media type / content type JSON je "application/json"*). JSON v těle požadavku bude vypadat následovně:
     `{"postalcode":"0300"}`
     Kde "0300" bude nahrazeno konkrétním kódem banky získaným z formuláře.
   * V případě nenalezení banky dojde k vrácení HTTP stavového kódu 404. V tomto případě uživatele nepřesměrovávejte, ale **zobrazte informaci o chybě přímo u formuláře** (vyplněná data musí zůstat zachována).
   * Pokud API odpoví úspěšně, **vrací XML**. Z dat vás zajímá pouze vlastnost `county`, která obsahuje název okresu dané instituce.

6. Po úspěšném ověření uložte veškerá data (Jméno, Email, Částku, Číslo účtu, Kód měny a Název okresu získaný z XML) do předpřipravené databáze SQLite.
   * *NuGET balíčky pro práci s SQLite: "Microsoft.Data.Sqlite.Core" a "SQLitePCLRaw.bundle_green".*
   * *Connection string pro databázi si zvolte vlastní, např. "Data Source=app.db".*

7. Zároveň dojde k uložení dat z formuláře do textového souboru na disk. Zápis do souboru bude proveden pomocí statické metody, které bude možné předat **libovolný objekt** a cestu k souboru, do kterého se má objekt uložit.
   * Název souboru bude obsahovat Jméno příjemce a Kód měny (např. `Jan_EUR.txt`).
   * V rámci této metody dojde k serializaci daného objektu s využitím **reflexe**.
   * Váš serializer bude serializovat **pouze vlastnosti, a to pouze ty, které jsou typu string** (ostatní typy není potřeba řešit).
   * Zápis musí zajistit, že se při opakovaném volání soubor kompletně přepíše čistými daty. Formát zápisu do textového souboru bude vypadat takto:
     ```text
     #nazevvlastnosti => hodnota;
     #Name => Jan;
     ... atd...
     ```
     Před názvem vlastnosti bude symbol hash, následovat bude "šipka", hodnota a na konci řádku středník.

8. Po úspěšném zápisu do databáze i souboru dojde k přesměrování zpět na úvodní obrazovku. Na úvodní obrazovce se následně vypíše potvrzovací zpráva (např. "Žádost byla úspěšně odeslána a uložena do souboru."). Tato zpráva po opětovném načtení stránky (F5) zmizí.