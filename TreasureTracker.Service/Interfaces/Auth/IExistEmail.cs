using TreasureTracker.Domain.Enums;
using TreasureTracker.Service.DTOs.Auth;
using TreasureTracker.Service.DTOs.Helpers;

namespace TreasureTracker.Service.Interfaces.Auth;
public interface IExistEmail
{
    Task<ExistEmailEnum> EmailExist(string email);

    Task SendMessage(Message message);

    Task<bool> VerifyCodeAsync(VerificationPostModel model);

    Task<bool> ResendCodeAsync(string email);
}
