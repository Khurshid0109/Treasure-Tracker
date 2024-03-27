using Microsoft.AspNetCore.Mvc;

namespace TreasureTracker.UI.Controllers
{
    public class PagesController : Controller
    {
        [HttpGet]
        public IActionResult AdminPage()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UserPage()
        {
            return View();
        }
    }
}
