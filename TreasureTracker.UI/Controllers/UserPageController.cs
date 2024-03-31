using Microsoft.AspNetCore.Mvc;
using TreasureTracker.Service.DTOs.Collections;
using TreasureTracker.Service.Interfaces.Collections;

namespace TreasureTracker.UI.Controllers;
public class UserPageController : Controller
{
    private readonly ICollectionService _collectionService;

    public UserPageController(ICollectionService collectionService)
    {
        _collectionService = collectionService;
    }

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

    [HttpGet]
    public IActionResult AddCollection()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddCollection(CollectionPostModel model)
    {
        if (ModelState.IsValid)
        {

        }
        return View();
    }
}
