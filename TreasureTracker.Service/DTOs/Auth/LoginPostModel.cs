using System.ComponentModel.DataAnnotations;

namespace TreasureTracker.Service.DTOs.Auth;
public class LoginPostModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
