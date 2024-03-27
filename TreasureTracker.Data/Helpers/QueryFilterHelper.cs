using Microsoft.EntityFrameworkCore;
using TreasureTracker.Domain.Commons;

namespace TreasureTracker.Data.Helpers;
public class QueryFilterHelper
{
    public static void AddQueryFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(Auditable).IsAssignableFrom(entityType.ClrType))
                modelBuilder.Entity(entityType.ClrType).AddQueryFilter<Auditable>(e => e.IsDeleted == false);
        }
    }
}
