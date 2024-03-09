using TreasureTracker.Domain.Commons;

namespace TreasureTracker.Domain.IRepositories;
public interface IRepository<T> where T : Auditable
{
}
