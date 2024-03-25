using TreasureTracker.Data.Db;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Domain.Repositories;

namespace TreasureTracker.Data.Repositories;
public class ItemTagRepository : Repository<ItemTag>, IItemTagRepository
{
    public ItemTagRepository(DataContext context) : base(context)
    {
    }
}
