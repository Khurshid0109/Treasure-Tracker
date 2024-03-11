using TreasureTracker.Data.Db;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Domain.Repositories;

namespace TreasureTracker.Data.Repositories;
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }

}
