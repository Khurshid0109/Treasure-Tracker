using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TreasureTracker.Domain.Commons;

namespace TreasureTracker.Domain.Entities;
public class UserCode:Auditable
{
    [ForeignKey(nameof(User))]
    public long UserId { get; set; }
    public User User { get; set; }

    [MaxLength(10)]
    public long Code { get; set; }

    public DateTime ExpireDate { get; set; }
}
