using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Service.DTOs.Categories;
using TreasureTracker.Service.DTOs.Users;
using TreasureTracker.Service.Interfaces.Categories;
using TreasureTracker.Service.Interfaces.Users;

namespace TreasureTracker.UI.Controllers
{
    public class PagesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryRepository _categoryRepository;

        public PagesController(IMapper mapper,
                              IUserRepository userRepository,
                              IUserService userService,
                              ICategoryService categoryService,
                              ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userService = userService;
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
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
    }
}
