namespace TreasureTracker.Service.DTOs.Comments;
public class CommentViewModel
{
    public long Id { get; set; }
    public string Text { get; set; }
    public long UserId { get; set; }
    public long ItemId { get; set; }
}
