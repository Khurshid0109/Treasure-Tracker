using TreasureTracker.Service.Helpers;

namespace TreasureTracker.Service.DTOs.Auth;
public class VerificationPostModel
{
    [TTrackerEmailAttribute(ErrorMessage = "Email is not valid.")]
    public string Email { get; set; }
    public string Code { get; set; }
}
