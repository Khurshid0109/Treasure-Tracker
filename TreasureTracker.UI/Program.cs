using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TreasureTracker.Data.Db;
using TreasureTracker.Service.Helpers.Media;
using TreasureTracker.Service.Mappers;
using TreasureTracker.Service.Services.Languages;
using TreasureTracker.UI.Extentions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
#region Localization
//Step 1
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
            return factory.Create("SharedResource", assemblyName.Name);
        };
    });

var supportedCultures = new[] { "en", "uz" }; // Add more as needed
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

#endregion

builder.Services.AddControllersWithViews();
builder.Services.AddServices();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();
WebHostEnvironmentHelper.WebRootPath = Path.GetFullPath("wwwroot");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(cors =>
    cors.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

//Step 2
app.UseRequestLocalization(localizationOptions);

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=ExistEmail}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
