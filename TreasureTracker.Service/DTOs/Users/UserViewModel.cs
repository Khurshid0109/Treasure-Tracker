using TreasureTracker.Domain.Enums;

namespace TreasureTracker.Service.DTOs.Users;
public class UserViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsVerified { get; set; } = false;
    public bool IsActive { get; set; }
    public Role Role { get; set; }
}
