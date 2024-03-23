using TreasureTracker.Service.DTOs.Users;

namespace TreasureTracker.Service.DTOs.Auth;
public class LoginViewModel
{
    public string Token { get; set; }
    public DateTime AccessTokenExpireDate { get; set; }
    public string RefreshToken { get; set; }
    public UserViewModel User { get; set; }
}
