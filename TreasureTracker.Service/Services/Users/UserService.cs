using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Domain.Enums;
using TreasureTracker.Domain.IRepositories;
using TreasureTracker.Service.Configurations;
using TreasureTracker.Service.DTOs.Helpers.Exceptions;
using TreasureTracker.Service.DTOs.Users;
using TreasureTracker.Service.Extentions;
using TreasureTracker.Service.Helpers.Hasher;
using TreasureTracker.Service.Interfaces.Auth;
using TreasureTracker.Service.Interfaces.Users;

namespace TreasureTracker.Service.Services.Users;
public class UserService:IUserService
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;
    private readonly IExistEmail _existEmail;

    public UserService(IRepository<User> repository, IMapper mapper, IExistEmail existEmail)
    {
        _repository = repository;
        _mapper = mapper;
        _existEmail = existEmail;
    }

    public async Task<UserViewModel> CreateAsync(UserPostModel model)
    {
        var user = await _repository.GetAllAsync()
            .Where(u => u.Email == model.Email)
            .FirstOrDefaultAsync();

        if (user is not null && !user.IsVerified)
            throw new TTrackerException(409, "Siz avval ro'yhatdan o'tgansiz, iltimos pochtangizni tasdiqlang va tizimga kiring!");

        if (user is not null)
            throw new TTrackerException(409, "Siz avval ro'yhatdan o'tgansiz, iltimos pochta va parol orqali tizimga kiring!");

        var mapped = _mapper.Map<User>(model);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.Password = HashPasswordHelper.PasswordHasher(model.Password);
        mapped.Role = Role.User;

        var result = await _repository.InsertAsync(mapped);
        await _repository.SaveChangesAsync();

        await _existEmail.ResendCodeAsync(model.Email);

        return _mapper.Map<UserViewModel>(result);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var user = await _repository.GetAllAsync()
             .Where(u => u.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (user is null)
            throw new TTrackerException(404, "User is not found!");

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params)
    {
        var users = await _repository.GetAllAsync()
              .ToPagedList<User>(@params)
              .ToListAsync();

        return _mapper.Map<IEnumerable<UserViewModel>>(users) ;
    }

    public async Task<UserViewModel> GetByEmailAsync(string email)
    {
        var user = await _repository.GetAllAsync()
             .Where(u => u.Email.ToLower() == email.ToLower())
             .FirstOrDefaultAsync();

        if (user is null)
            throw new TTrackerException(404, "User is not found!");

        return _mapper.Map<UserViewModel>(user);
    }

    public async Task<UserViewModel> GetByIdAsync(long id)
    {
        var user = await _repository.GetAllAsync()
              .Where(u => u.Id == id)
              .FirstOrDefaultAsync();

        if (user is null)
            throw new TTrackerException(404, "User is not found!");

        return _mapper.Map<UserViewModel>(user);
    }

    public async Task<UserViewModel> UpdateAsync(long id, UserPostModel model)
    {
        var user = await _repository.GetAllAsync()
             .Where(u => u.Id == id)
             .FirstOrDefaultAsync();

        if (user is null)
            throw new TTrackerException(404,"User is not found!");

        var mapped = _mapper.Map(model, user);
        mapped.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAasync(mapped);
        await _repository.SaveChangesAsync();

        return _mapper.Map<UserViewModel>(mapped);
    }
}
