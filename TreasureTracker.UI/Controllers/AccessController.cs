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
                try
                {
                    var result = await _authService.AuthenticateAsync(model);

                    // Check if the login was successful and an access token is available
                    if (!string.IsNullOrEmpty(result.Token))
                    {
                        // Set a cookie with the access token
                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = result.AccessTokenExpireDate
                        };

                        Response.Cookies.Append("token", result.Token, cookieOptions);

                        // Redirect to the desired page after successful login
                        return Redirect("~/Home/Index");
                    }

                    // Handle the case when login was not successful
                    ModelState.AddModelError("", "Login failed");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult ExistEmail()
        {
            return View();
        }

        [HttpPost("ExistEmail")]
        public async Task<IActionResult> ExistEmail(ExistEmailPostModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Email"] = model.Email;

                var result = await _checkerService.EmailExist(model.Email);

                if (result is ExistEmailEnum.EmailNotFound)
                    return Redirect("~/Access/Register");

                else if (result is ExistEmailEnum.EmailNotChecked)
                    return Redirect("~/Access/Verification");

                return Redirect("~/Access/Login");
            }

            return View(model);
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
                try
                {
                    var result = await _authService.CreateAsync(model);

                    // Check if the registration was successful and an access token is available
                    if (!string.IsNullOrEmpty(result.Token))
                    {
                        // Set a cookie with the access token
                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = result.AccessTokenExpireDate
                        };

                        Response.Cookies.Append("token", result.Token, cookieOptions);

                        // Redirect to the desired page after successful registration
                        return Redirect("~/Access/Verification");
                    }

                    // Handle the case when registration was not successful
                    ModelState.AddModelError("", "Registration failed");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
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

            return View(model);
        }
    }
}
