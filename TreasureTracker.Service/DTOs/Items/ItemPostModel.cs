using Microsoft.AspNetCore.Http;
using TreasureTracker.Domain.Entities;

namespace TreasureTracker.Service.DTOs.Items;
public class ItemPostModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile ImageUrl { get; set; }
    public long CollectionId { get; set; }
    public Collection Collection { get; set; }
}
