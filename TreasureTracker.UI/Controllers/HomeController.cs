using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using TreasureTracker.Service.Services.Languages;

namespace TreasureTracker.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  LanguageService _localization;

        public HomeController(ILogger<HomeController> logger, LanguageService localization)
        {
            _logger = logger;
            _localization = localization;
        }

        public IActionResult Index()
        {
            //get culture information
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
            return View();
        }

        #region Localization
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
               new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString());
            //return await Task.FromResult(RedirectToAction("Index"));
        }
        #endregion

    }
}
