using TreasureTracker.Domain.Commons;

namespace TreasureTracker.Domain.Entities;
public class Comment:Auditable
{
    public string Text { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
}
