using TreasureTracker.Domain.Enums;

namespace TreasureTracker.Service.DTOs.Users;
public class UserViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsVerified { get; set; } = false;
    public bool IsActive { get; set; }
    public Role Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

}
