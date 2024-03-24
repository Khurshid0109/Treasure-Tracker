using Microsoft.AspNetCore.Mvc;
using TreasureTracker.Domain.Enums;
using TreasureTracker.Service.DTOs.Auth;
using TreasureTracker.Service.DTOs.Users;
using TreasureTracker.Service.Interfaces.Auth;

namespace TreasureTracker.UI.Controllers
{
    public class AccessController : Controller
    {
        private readonly IUserAuthentication _authService;
        private readonly IExistEmail _checkerService;

        public AccessController(IUserAuthentication authService, 
                                IExistEmail checkerService)
        {
            _authService = authService;
            _checkerService = checkerService;
        }

        [HttpGet]
        public ViewResult Login()
        {
            // Retrieve the email from TempData
            string? userEmail = TempData["Email"] as string;

            // Pass the email to the view
            ViewBag.Email = userEmail;

            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginPostModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.AuthenticateAsync(model);

                if(result is not null)
                    return Redirect("~/Home/Index");
            }

            return View();
        }

        [HttpGet]
        public ViewResult ExistEmail()
        {
            return View();
        }

        [HttpPost("ExistEmail")]
        public async Task<IActionResult> ExistEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                TempData["Email"] = email;

                var result = await _checkerService.EmailExist(email);

                if (result is ExistEmailEnum.EmailNotFound)
                    return Redirect("~/Access/Register");

                else if (result is ExistEmailEnum.EmailNotChecked)
                    return Redirect("~/Access/Verification");

                return Redirect("~/Access/Login");
            }

            return View();
        }

        [HttpGet]
        public ViewResult Register()
        {
            // Retrieve the email from TempData
            string? userEmail = TempData["Email"] as string;

            // Pass the email to the view
            ViewBag.Email = userEmail;

            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserPostModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.CreateAsync(model);
                return Redirect("~/Access/Verification");
            }
            return View();
        }

        [HttpGet]
        public ViewResult Verification()
        {
            // Retrieve the email from TempData
            string? userEmail = TempData["Email"] as string;

            // Pass the email to the view
            ViewBag.Email = userEmail;

            return View();
        }

        [HttpPost("verification")]
        public async Task<IActionResult> Verification(VerificationPostModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _checkerService.VerifyCodeAsync(model);

                if(result)
                    return Redirect("~/Home/Index");
            }

            return View();
        }
    }
}
