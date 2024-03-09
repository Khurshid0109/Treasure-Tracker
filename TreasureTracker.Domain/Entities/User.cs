using TreasureTracker.Domain.Enums;
using TreasureTracker.Domain.Commons;

namespace TreasureTracker.Domain.Entities;
public class User:Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsVerified { get; set; } = false;
    public bool IsActive { get; set; }
    public Role Role { get; set; }
}
