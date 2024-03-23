using TreasureTracker.Domain.Entities;
using TreasureTracker.Domain.Enums;
using TreasureTracker.Service.DTOs.Auth;
using TreasureTracker.Service.DTOs.Helpers;
using TreasureTracker.Service.DTOs.Users;

namespace TreasureTracker.Service.Interfaces.Auth;
public interface IUserAuthentication
{
    Task<EmailExistance> CheckEmail(string email);
    Task<string> Login(LoginViewModel model);
    Task<string> Register(UserPostModel model);
    public Task<bool> GenerateCode(User user);
    public Task SendMessage(Message message);
}
