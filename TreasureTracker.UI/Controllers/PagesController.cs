using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Service.DTOs.Users;

namespace TreasureTracker.UI.Controllers
{
    public class PagesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public PagesController(IMapper mapper, 
                              IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
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
    }
}
