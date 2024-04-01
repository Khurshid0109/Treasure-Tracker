using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Service.DTOs.Users;
using TreasureTracker.Service.DTOs.Categories;
using TreasureTracker.Service.Interfaces.Users;
using TreasureTracker.Service.Interfaces.Categories;
using TreasureTracker.Service.Interfaces.Collections;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Service.DTOs.Collections;

namespace TreasureTracker.UI.Controllers
{
    public class PagesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly ICollectionService _collectionService;

        public PagesController(IMapper mapper,
                              IUserRepository userRepository,
                              IUserService userService,
                              ICategoryService categoryService,
                              ICategoryRepository categoryRepository,
                              ICollectionRepository collectionRepository,
                              ICollectionService collectionService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userService = userService;
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
            _collectionRepository = collectionRepository;
            _collectionService = collectionService;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UsersList()
        {
            var users = await _userRepository.GetAllAsync()
                .IgnoreQueryFilters()
                .ToListAsync();
            IEnumerable<UserViewModel> usersViewModel = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return View(usersViewModel);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserPostModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateAsync(model);
                return Redirect("~/Pages/UsersList");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CategoriesList()
        {
            var categories = await _categoryRepository.GetAllAsync()
                .AsNoTracking()
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return View(result);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryPostModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateAsync(model);
                return Redirect("~/Pages/CategoriesList");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CollectionsList()
        {
            var collections = await _collectionRepository.GetAllAsync()
                .AsNoTracking()
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<CollectionViewModel>>(collections);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> PerCollectionsList()
        {
            var userId = GetUserId();

            if(userId==0 )
                return Redirect("~/Access/existemail");

            var collections = await _collectionRepository.GetAllAsync()
                .Where(c => c.UserId == userId)
                .AsNoTracking()
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<CollectionViewModel>>(collections);
            return View(result);
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
                var userId = GetUserId();

                if(userId==0 )
                    return Redirect("~/Access/existemail");

                model.UserId = userId;
                await _collectionService.CreateAsync(model);
                return Redirect("~/Pages/CollectionsList");
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
}
