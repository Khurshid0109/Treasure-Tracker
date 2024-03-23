using TreasureTracker.Service.DTOs.Users;

namespace TreasureTracker.Service.Interfaces.Auth;
public interface IJwtTokenService
{
    Task<(string token, DateTime tokenExpiryTime)> GenerateTokenAsync(UserViewModel user);
    Task<(string refreshToken, DateTime tokenValidityTime)> GenerateRefreshTokenAsync();
    Task<UserViewModel?> GetUserByAccessTokenAsync(string accessToken);
}
