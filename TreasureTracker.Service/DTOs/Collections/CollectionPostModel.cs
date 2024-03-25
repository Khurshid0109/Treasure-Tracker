using Microsoft.AspNetCore.Http;

namespace TreasureTracker.Service.DTOs.Collections;
public class CollectionPostModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile ImageUrl { get; set; }
    public long UserId { get; set; }
    public long CategoryId { get; set; }
}
