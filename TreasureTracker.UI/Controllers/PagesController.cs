using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Service.DTOs.Users;
using TreasureTracker.Service.Interfaces.Users;

namespace TreasureTracker.UI.Controllers
{
    public class PagesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public PagesController(IMapper mapper,
                              IUserRepository userRepository,
                              IUserService userService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UsersList()
        {
            var users = await _userRepository.GetAllAsync().ToListAsync();
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
            return View();
        }
    }
}
