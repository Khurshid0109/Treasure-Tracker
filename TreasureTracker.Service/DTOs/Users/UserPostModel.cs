using System.ComponentModel.DataAnnotations;
using TreasureTracker.Service.Helpers;

namespace TreasureTracker.Service.DTOs.Users;
public class UserPostModel
{
    [Required]
    [MinLength(3),MaxLength(10)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(3), MaxLength(10)]
    public string LastName { get; set; }
    [Required]
    [TTrackerEmailAttribute(ErrorMessage = "Email is not valid.")]
    public string Email { get; set; }
    [Required]
    [MinLength(6), MaxLength(10)]
    public string Password { get; set; }
}
