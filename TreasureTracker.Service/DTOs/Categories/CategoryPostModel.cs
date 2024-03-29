using System.ComponentModel.DataAnnotations;

namespace TreasureTracker.Service.DTOs.Categories;
public class CategoryPostModel
{
    [Required]
    [MinLength(3), MaxLength(10)]
    public string Name { get; set; }
}
