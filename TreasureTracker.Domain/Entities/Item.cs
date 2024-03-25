using TreasureTracker.Domain.Commons;

namespace TreasureTracker.Domain.Entities;
public class Item:Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public int CollectionId { get; set; }
    public Collection Collection { get; set; }
    public ICollection<string> Tags { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
