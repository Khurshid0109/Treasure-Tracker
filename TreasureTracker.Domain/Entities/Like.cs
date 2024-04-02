using TreasureTracker.Domain.Commons;

namespace TreasureTracker.Domain.Entities;
public class Like:Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long ItemId { get; set; }
    public Item Item { get; set; }
}
