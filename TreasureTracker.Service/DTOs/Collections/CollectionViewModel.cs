namespace TreasureTracker.Service.DTOs.Collections;
public class CollectionViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public long CategoryId { get; set; }
    public long UserId { get; set; }
}
