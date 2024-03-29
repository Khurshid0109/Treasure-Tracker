using System.ComponentModel.DataAnnotations;

namespace TreasureTracker.Service.DTOs.UserCodes;
public class UserCodePostModel
{
    public long UserId { get; set; }
    [Required]
    [MinLength(6),MaxLength(6)]
    public string Code { get; set; }
}
