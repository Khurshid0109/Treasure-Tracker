using TreasureTracker.Domain.Commons;

namespace TreasureTracker.Domain.Entities;
public class Tag:Auditable
{
    public string Name { get; set; }

    // Navigation property for the many-to-many relationship with items
    public ICollection<ItemTag> ItemTags { get; set; }
}
