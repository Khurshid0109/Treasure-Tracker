using TreasureTracker.Data.Db;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Domain.Repositories;

namespace TreasureTracker.Data.Repositories;
public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(DataContext context) : base(context)
    {
    }
}
