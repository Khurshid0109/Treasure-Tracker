using Microsoft.AspNetCore.Mvc;

namespace TreasureTracker.UI.Controllers;
public class UserPageController : Controller
{
    [HttpGet]
    public IActionResult Dashboard()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Collections()
    {
        return View();
    }
}
