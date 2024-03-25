using TreasureTracker.Domain.Commons;

namespace TreasureTracker.Domain.Entities;
public class Collection:Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Item> Items { get; set; }
}
