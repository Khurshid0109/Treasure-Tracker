using TreasureTracker.Data.Db;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Domain.Repositories;

namespace TreasureTracker.Data.Repositories;
public class ItemRepository : Repository<Item>, IItemRepository
{
    public ItemRepository(DataContext context) : base(context)
    {
    }
}
