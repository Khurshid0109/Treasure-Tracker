using System.ComponentModel.DataAnnotations;
using TreasureTracker.Service.Helpers;

namespace TreasureTracker.Service.DTOs.Auth;
public class VerificationPostModel
{
    [Required(ErrorMessage = "Email is required.")]
    [TTrackerEmailAttribute(ErrorMessage = "Email is not valid.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Code is required.")]
    [MinLength(6, ErrorMessage = "Code must be 6 characters long.")]
    [MaxLength(8,ErrorMessage = "Code must be 6 characters long.")]
    public string Code { get; set; }
}
