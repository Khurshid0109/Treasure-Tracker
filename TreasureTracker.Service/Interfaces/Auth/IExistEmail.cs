using TreasureTracker.Domain.Enums;
using TreasureTracker.Service.DTOs.Helpers;

namespace TreasureTracker.Service.Interfaces.Auth;
public interface IExistEmail
{
    Task<ExistEmailEnum> EmailExistance(string email);

    Task SendMessage(Message message);

    Task<bool> VerifyCodeAsync(string email, long code);

    Task<bool> ResendCodeAsync(string email);
}
