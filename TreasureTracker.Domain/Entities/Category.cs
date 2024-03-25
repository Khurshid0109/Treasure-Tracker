using TreasureTracker.Domain.Commons;

namespace TreasureTracker.Domain.Entities;
public class Category:Auditable
{
    public string Name { get; set; }
    public ICollection<Collection> Collections { get; set; }
}
