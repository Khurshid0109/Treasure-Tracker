using TreasureTracker.Data.Db;
using Microsoft.EntityFrameworkCore;
using TreasureTracker.Domain.Commons;
using TreasureTracker.Domain.IRepositories;

namespace TreasureTracker.Domain.Repositories;
public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly DataContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
       var user = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
       _dbSet.Remove(user);
        return true;
    }

    public IQueryable<T> GetAllAsync()
    => _dbSet.AsQueryable();

    public async Task<T> GetByIdAsync(long id)
    => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<T> InsertAsync(T entity)
    =>(await _dbSet.AddAsync(entity)).Entity;

    public async Task<bool> SaveChangesAsync()
    => await _context.SaveChangesAsync() > 0;

    public async Task<T> UpdateAasync(T entity)
    {
       var entry = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
       _dbSet.Update(entity);
       return entity;
    }
}
