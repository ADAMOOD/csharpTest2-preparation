using Dapper;
using Menza_XML.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();//registrovani http clienta

string conncectionString = builder.Configuration.GetConnectionString("DefaultConnection"); //vytahnu z konfigurace muj conncectionstring 
builder.Services.AddScoped<DbService>(provider => new DbService(conncectionString)); //dam service s mym conncetionstringem

builder.Services.AddScoped<ApiService>();

var app = builder.Build();


//zase musim nastavit dialect na sqlite
Dapper.SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);
//tim ze jsem zaregistroval DBService jako scoped tak ji musim vytvorit scope
using (var scope = app.Services.CreateScope())
{
    // Získáme DbService z poskytovatele v rámci scope
    var dbService = scope.ServiceProvider.GetRequiredService<DbService>();

    // Spustíme asynchronní inicializaci tabulky
    await dbService.init();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
