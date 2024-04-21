﻿using TreasureTracker.Domain.Commons;
using TreasureTracker.Domain.Enums;

namespace TreasureTracker.Domain.Entities;
public class User:Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsVerified { get; set; } = false;
    public Role Role { get; set; }

    public string RefreshToken { get; set; }
    public DateTime ExpireDate { get; set; }

    public ICollection<UserCode> UserCodes { get; set; }
    public ICollection<Collection> Collections { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
