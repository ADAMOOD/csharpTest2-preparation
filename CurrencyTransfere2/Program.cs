using CurrencyTransfere2.Services;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();//zaregistrovani sluzby HTTPClient

builder.Services.AddTransient<CurrencyService>();//zaregistrovani ME sluzby pro praci s klientem

SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);//tohle nastavi dialect simpleCRUDU NA SQLite
var conncectionString = builder.Configuration.GetConnectionString("DefaultConnection");//tady ziskame connection string z appsettings.json
builder.Services.AddScoped<DatabaseService>(provider=> new DatabaseService(conncectionString));//tady vytvorime objekt 

var app = builder.Build();

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
