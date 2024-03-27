using Microsoft.EntityFrameworkCore;
using TreasureTracker.Data.Helpers;
using TreasureTracker.Domain.Entities;

namespace TreasureTracker.Data.Db;
public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCode> UserCodes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag>  Tags { get; set; }
    public DbSet<ItemTag> ItemTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        QueryFilterHelper.AddQueryFilters(modelBuilder);
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasQueryFilter(user => user.IsVerified);
    }
}
