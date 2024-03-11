using Microsoft.EntityFrameworkCore;
using TreasureTracker.Domain.Entities;

namespace TreasureTracker.Data.Db;
public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {
    }
    public DbSet<User> Users { get; set; }
}
