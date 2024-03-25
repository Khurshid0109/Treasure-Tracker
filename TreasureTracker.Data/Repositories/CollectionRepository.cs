using TreasureTracker.Data.Db;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Domain.Repositories;

namespace TreasureTracker.Data.Repositories;
public class CollectionRepository : Repository<Collection>, ICollectionRepository
{
    public CollectionRepository(DataContext context) : base(context)
    {
    }
}
