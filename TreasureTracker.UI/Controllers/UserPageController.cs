using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Service.DTOs.Categories;
using TreasureTracker.Service.DTOs.Collections;
using TreasureTracker.Service.Interfaces.Collections;

namespace TreasureTracker.UI.Controllers;
public class UserPageController : Controller
{
    private readonly ICollectionService _collectionService;
    private readonly ICollectionRepository _collectionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UserPageController(ICollectionService collectionService,
                              ICategoryRepository categoryRepository,
                              IMapper mapper,
                              ICollectionRepository collectionRepository)
    {
        _collectionService = collectionService;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _collectionRepository = collectionRepository;
    }

    [HttpGet]
    public IActionResult Dashboard()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Collections()
    {
        var collections = await _collectionRepository.GetAllAsync()
            .AsNoTracking()
            .ToListAsync();
        var result = _mapper.Map<IEnumerable<CollectionViewModel>>(collections);
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryRepository.GetAllAsync().ToListAsync();
        var result = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        return Json(result);
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
            try
            {
                model.UserId = GetUserId();
                await _collectionService.CreateAsync(model);
                return Redirect("~/UserPage/Collections");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

        }
        return View(model);
    }

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
