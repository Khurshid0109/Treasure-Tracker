using TreasureTracker.Service.Helpers;
using System.ComponentModel.DataAnnotations;

namespace TreasureTracker.Service.DTOs.Users;
public class UserPostModel
{
    [Required(ErrorMessage ="Name is required")]
    [MinLength(3,ErrorMessage ="It should be at least 3 characters.")]
    [MaxLength(10,ErrorMessage ="It should be maximum 10 characters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage ="Name is required.")]
    [MinLength(3,ErrorMessage = "It should be at least 3 characters.")]
    [MaxLength(10,ErrorMessage = "It should be maximum 10 characters.")]
    public string LastName { get; set; }
    [Required(ErrorMessage ="Email is required.")]
    [TTrackerEmailAttribute(ErrorMessage = "Email is not valid.")]
    public string Email { get; set; }
    [Required(ErrorMessage ="Password is required.")]
    [MinLength(6,ErrorMessage ="It should contain at least 6 characters.")]
    [MaxLength(10,ErrorMessage = "It should contain maximum 10 characters.")]
    public string Password { get; set; }
}
