using Microsoft.AspNetCore.Mvc;

namespace TreasureTracker.UI.Controllers;
public class ErrorController : Controller
{
    [Route("/Error/GlobalError")]
    public IActionResult GlobalError(int statusCode = 500)
    {
        ViewData["StatusCode"] = statusCode;

        return View();
    }
}
