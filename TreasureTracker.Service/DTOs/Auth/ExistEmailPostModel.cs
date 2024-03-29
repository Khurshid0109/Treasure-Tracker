using System.ComponentModel.DataAnnotations;
using TreasureTracker.Service.Helpers;

namespace TreasureTracker.Service.DTOs.Auth;
public class ExistEmailPostModel
{
    [Required(ErrorMessage ="Email is required")]
    [TTrackerEmailAttribute(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
}
