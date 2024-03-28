using TreasureTracker.Service.Configurations;
using TreasureTracker.Service.DTOs.Collections;

namespace TreasureTracker.Service.Interfaces.Collections;
public interface ICollectionService
{
    Task<bool> DeleteAsync(long id);
    Task<CollectionViewModel> GetByIdAsync(long id);
    Task<IEnumerable<CollectionViewModel>> GetAllAsync(PaginationParams @params);
    Task<CollectionViewModel> CreateAsync(CollectionPostModel model);
    Task<CollectionViewModel> UpdateAsync(long id, CollectionPostModel model);
}
