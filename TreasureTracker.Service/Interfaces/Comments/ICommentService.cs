using TreasureTracker.Service.Configurations;
using TreasureTracker.Service.DTOs.Comments;

namespace TreasureTracker.Service.Interfaces.Comments;
public interface ICommentService
{
    Task<bool> DeleteAsync(long id);
    Task<CommentViewModel> GetByIdAsync(long id);
    Task<IEnumerable<CommentViewModel>> GetAllAsync(PaginationParams @params);
    Task<CommentViewModel> CreateAsync(CommentPostModel model);
    Task<CommentViewModel> UpdateAsync(long id, CommentPostModel model);
}
