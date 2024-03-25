using TreasureTracker.Data.Db;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Domain.Repositories;

namespace TreasureTracker.Data.Repositories;
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(DataContext context) : base(context)
    {
    }
}
