using TreasureTracker.Domain.Commons;

namespace TreasureTracker.Domain.Entities;
public class Item:Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public long CollectionId { get; set; }
    public Collection Collection { get; set; }
    public ICollection<ItemTag> ItemTags { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
