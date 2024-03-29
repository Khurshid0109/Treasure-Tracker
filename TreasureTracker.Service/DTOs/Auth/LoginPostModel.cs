using System.ComponentModel.DataAnnotations;
using TreasureTracker.Service.Helpers;

namespace TreasureTracker.Service.DTOs.Auth;
public class LoginPostModel
{
    [Required]
    [TTrackerEmailAttribute(ErrorMessage = "Email is not valid.")]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
