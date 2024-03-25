using TreasureTracker.Domain.Commons;

namespace TreasureTracker.Domain.Entities;
public class ItemTag:Auditable
{
    public long ItemId { get; set; }
    public Item Item { get; set; }

    public long TagId { get; set; }
    public Tag Tag { get; set; }
}
