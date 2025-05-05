using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Survey.DAL;
using Survey.BLL;

var builder = WebApplication.CreateBuilder(args);

// (Code Scan analyzers can be included in the project file for static analysis.)

// Add services to the container.
builder.Services.AddRazorPages();

// Add session support.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Auto-detect environment and choose repository implementation.
// Set "UseTestData" in appsettings.json (or override via environment) to true if SQL is not available.
var useTestData = builder.Configuration.GetValue<bool>("UseTestData");
if (useTestData)
{
    builder.Services.AddScoped<IRepository, JsonRepository>();
}
else
{
    builder.Services.AddScoped<IRepository, Repository>();
}

// Register the business service.
builder.Services.AddScoped<IService, Service>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();