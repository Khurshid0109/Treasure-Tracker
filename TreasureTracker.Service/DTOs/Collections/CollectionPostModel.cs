using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TreasureTracker.Service.DTOs.Collections;
public class CollectionPostModel
{
    [Required]
    [MinLength(3), MaxLength(10)]
    public string Name { get; set; }
    [Required]
    [MinLength(10)]
    public string Description { get; set; }
    [Required]
    public IFormFile ImageUrl { get; set; }
    public long UserId { get; set; }
    public long CategoryId { get; set; }
}
