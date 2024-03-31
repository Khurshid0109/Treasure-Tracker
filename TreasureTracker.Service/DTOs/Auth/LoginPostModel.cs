using System.ComponentModel.DataAnnotations;
using TreasureTracker.Service.Helpers;

namespace TreasureTracker.Service.DTOs.Auth;
public class LoginPostModel
{
    [Required(ErrorMessage ="Email is required.")]
    [TTrackerEmailAttribute(ErrorMessage = "Email is not valid.")]
    public string Email { get; set; }

    [Required(ErrorMessage ="Email is required")]
    [MinLength(5,ErrorMessage ="Password should be at least 6 charactes.")]
    public string Password { get; set; }
}
