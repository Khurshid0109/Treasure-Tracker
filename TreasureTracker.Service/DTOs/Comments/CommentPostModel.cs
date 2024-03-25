namespace TreasureTracker.Service.DTOs.Comments;
public class CommentPostModel
{
    public string Text { get; set; }
    public long UserId { get; set; }
    public long ItemId { get; set; }
}
