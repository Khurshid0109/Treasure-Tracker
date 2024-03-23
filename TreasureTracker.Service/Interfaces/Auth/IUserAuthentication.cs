using TreasureTracker.Service.DTOs.Auth;
using TreasureTracker.Service.DTOs.Users;

namespace TreasureTracker.Service.Interfaces.Auth;
public interface IUserAuthentication
{
    public Task<LoginViewModel> AuthenticateAsync(LoginPostModel login);
    public Task<LoginViewModel> CreateAsync(UserPostModel user);
    public Task<bool> ChangePassword(string email, string password);
}
