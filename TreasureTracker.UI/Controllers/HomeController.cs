using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Service.Interfaces.Users;
using TreasureTracker.Service.Services.Languages;
using TreasureTracker.UI.ViewModels;

namespace TreasureTracker.UI.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  LanguageService _localization;
        private readonly IUserService _userService;
        private readonly ICollectionRepository _repository;

        public HomeController(ILogger<HomeController> logger,
                              LanguageService localization,
                              IUserService userService,
                              ICollectionRepository repository)
        {
            _logger = logger;
            _localization = localization;
            _userService = userService;
            _repository = repository;
        }
        

        [HttpGet]
        public  async Task<IActionResult> Index()
        {
            var id = GetUserId();

            var user = await _userService.GetByIdAsync(id);

            var collections = await _repository.GetAllAsync()
                .Include(c=>c.User)
                .ToListAsync();

            //get culture information
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;

            var model = new IndexViewModel
            {
                User = user,
                Collections = collections
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CollectionView(long id)
        {
            var userId = GetUserId();
            var user = await _userService.GetByIdAsync(userId);

            var collection = await _repository.GetAllAsync()
                .Where(c => c.Id == id)
                .Include(c => c.User)
                .Include(c => c.Items)
                .ThenInclude(i => i.Comments)
                .Include(c => c.Items)
                .ThenInclude(i => i.Likes)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            var model = new CollectionPageViewModel
            {
                User = user,
                Collection = collection
            };

            return View(model);
        }

        public async Task<IActionResult> RedirectUser()
        {
            var id = GetUserId();
            var user = await _userService.GetByIdAsync(id);

            if (user.Role == Domain.Enums.Role.User)
                return Redirect("~/UserPage/Dashboard");

            return Redirect("~/Pages/Dashboard");
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
