using System.ComponentModel.DataAnnotations;

namespace TreasureTracker.Service.DTOs.Comments;
public class CommentPostModel
{
    [Required]
    [MinLength(2)]
    public string Text { get; set; }
    public long UserId { get; set; }
    public long ItemId { get; set; }
}
