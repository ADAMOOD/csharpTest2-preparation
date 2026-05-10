using FinalGeminiBossFight.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();//zaregistrovani http clienta 

builder.Services.AddScoped<CurrencyApiService>();//zaregistrovani api

var connectionString = builder.Configuration.GetConnectionString("defaultConnection");

builder.Services.AddScoped<DbService>(provider => new DbService(connectionString));//predani connectionstringu

Dapper.SimpleCRUD.SetDialect(Dapper.SimpleCRUD.Dialect.SQLite);

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var dbservice = scope.ServiceProvider.GetRequiredService<DbService>();
    await dbservice.InitTableAsync();//zavolani initu ve scope
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
