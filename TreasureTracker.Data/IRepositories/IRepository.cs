using TreasureTracker.Domain.Commons;

namespace TreasureTracker.Domain.IRepositories;
public interface IRepository<T> where T : Auditable
{
    Task<bool> DeleteAsync(long id);
    Task<T> InsertAsync(T entity);
    Task<T> UpdateAasync(T entity);
    Task<T> GetByIdAsync(long id);
    IQueryable<T> GetAllAsync();
    Task<bool> SaveChangesAsync();
}
