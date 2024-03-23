using TreasureTracker.Data.Db;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Domain.Repositories;

namespace TreasureTracker.Data.Repositories;
public class UserCodeRepository:Repository<UserCode>, IUserCodeRepository
{
    public UserCodeRepository(DataContext context) : base(context)
    {
    }
}
