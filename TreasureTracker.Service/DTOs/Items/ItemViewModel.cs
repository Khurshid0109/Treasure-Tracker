using TreasureTracker.Domain.Entities;

namespace TreasureTracker.Service.DTOs.Items;
public class ItemViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public long CollectionId { get; set; }
    public Collection Collection { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Like> Likes { get; set; }
}
