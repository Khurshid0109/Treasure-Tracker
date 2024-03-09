using TreasureTracker.Domain.Commons;
using TreasureTracker.Domain.IRepositories;

namespace TreasureTracker.Domain.Repositories;
public class Repository<T> where T : Auditable, IRepository<T> 
{
}
