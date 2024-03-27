using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TreasureTracker.Service.Interfaces.Users;
using TreasureTracker.Service.Services.Languages;

namespace TreasureTracker.UI.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  LanguageService _localization;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger,
                              LanguageService localization, 
                              IUserService userService)
        {
            _logger = logger;
            _localization = localization;
            _userService = userService;
        }

        [HttpGet]
        public  async Task<IActionResult> Index()
        {
            var id = GetUserId();

            var user = await _userService.GetByIdAsync(id);
            //get culture information
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
            return View(user);
        }

        public async Task<IActionResult> RedirectUser()
        {
            var id = GetUserId();
            var user = await _userService.GetByIdAsync(id);

            if (user.Role == Domain.Enums.Role.User)
                return Redirect("~/Pages/UserPage");

            return Redirect("~/Pages/AdminPage");
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

        private long GetUserId()
        {
            var token = HttpContext.Request.Cookies["token"];

            if (string.IsNullOrEmpty(token))
                return 0;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var userId = jwtToken.Claims.First(claim => claim.Type == "Id").Value;
                return long.Parse(userId);
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}
