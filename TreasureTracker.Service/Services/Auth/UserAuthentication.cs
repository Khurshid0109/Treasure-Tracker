using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Service.DTOs.Auth;
using TreasureTracker.Service.DTOs.Helpers.Exceptions;
using TreasureTracker.Service.DTOs.Users;
using TreasureTracker.Service.Helpers.Hasher;
using TreasureTracker.Service.Interfaces.Auth;
using TreasureTracker.Service.Interfaces.Users;

namespace TreasureTracker.Service.Services.Auth;
public class UserAuthentication : IUserAuthentication
{
    private readonly IMapper _mapper;
    private readonly IExistEmail _existEmail;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserRepository _repository;
    private readonly IUserService _userService;

    public UserAuthentication(IUserRepository repository,
                      IJwtTokenService jwtTokenService,
                      IMapper mapper,
                      IExistEmail existEmail,
                      IUserService userService)
    {
        _repository = repository;
        _mapper = mapper;
        _jwtTokenService = jwtTokenService;
        _existEmail = existEmail;
        _userService = userService;
    }
    public async Task<LoginViewModel> AuthenticateAsync(LoginPostModel login)
    {
        var user = await _repository.GetAllAsync()
              .IgnoreQueryFilters()
              .Where(u => u.Email.ToLower() == login.Email.ToLower())
              .FirstOrDefaultAsync();

        if (user is null || !HashPasswordHelper.IsEqual(login.Password, user.Password))
            throw new TTrackerException(404, "Email yoki parol xato!");

        if (user.IsDeleted)
            throw new TTrackerException(403, "Sizning hisobingiz bloklangan!");

        if (!user.IsVerified)
            throw new TTrackerException(403, "Iltimos avval pochtangizni tasdiqlang!");

        if (user.ExpireDate > DateTime.UtcNow)
        {
            (user.RefreshToken, user.ExpireDate) = await _jwtTokenService.GenerateRefreshTokenAsync();
            await _userService.UpdateAsync(user.Id, _mapper.Map<UserPostModel>(user));
        }

        var userView = _mapper.Map<UserViewModel>(user);
        (string token, DateTime expireDate) = await _jwtTokenService.GenerateTokenAsync(userView);
        return new LoginViewModel
        {
            Token = token,
            AccessTokenExpireDate = expireDate,
            RefreshToken = user.RefreshToken,
            User = userView
        };
    }

    public async Task<LoginViewModel> CreateAsync(UserPostModel model)
    {
        var user = await _repository.GetAllAsync()
            .IgnoreQueryFilters()
            .Where(u => u.Email.ToLower() == model.Email.ToLower())
            .FirstOrDefaultAsync();

        if (user is not null && !user.IsVerified)
            throw new TTrackerException(409, "Siz avval ro'yhatdan o'tgansiz, iltimos pochtangizni tasdiqlang va tizimga kiring!");

        if (user is not null)
            throw new TTrackerException(409, "Siz avval ro'yhatdan o'tgansiz, iltimos pochta va parol orqali tizimga kiring!");

        var mapped = _mapper.Map<User>(model);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.Password = HashPasswordHelper.PasswordHasher(model.Password);
        (mapped.RefreshToken, mapped.ExpireDate) = await _jwtTokenService.GenerateRefreshTokenAsync();

        var result = await _repository.InsertAsync(mapped);
        await _repository.SaveChangesAsync();

        await _existEmail.ResendCodeAsync(model.Email);

        var userView = _mapper.Map<UserViewModel>(result);
        (string token, DateTime expireDate) = await _jwtTokenService.GenerateTokenAsync(userView);
        return new LoginViewModel
        {
            Token = token,
            AccessTokenExpireDate = expireDate,
            RefreshToken = mapped.RefreshToken,
            User = userView
        };
    }

    public async Task<bool> ChangePassword(string email, string password)
    {
        var user = await _repository.GetAllAsync()
             .Where(u => u.Email == email)
             .FirstOrDefaultAsync();

        if (user is null || HashPasswordHelper.IsEqual(Constants.PASSWORD_SALT, user.Password))
            return false;

        user.Password = HashPasswordHelper.PasswordHasher(password);
        await _repository.SaveChangesAsync();
        return true;
    }
}
