using TreasureTracker.Data.Db;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Domain.Repositories;

namespace TreasureTracker.Data.Repositories;
public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(DataContext context) : base(context)
    {
    }
}
