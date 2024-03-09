using Microsoft.EntityFrameworkCore;

namespace TreasureTracker.Data.Db;
public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {
    }

}
