using TreasureTracker.Service.Configurations;
using TreasureTracker.Service.DTOs.Items;

namespace TreasureTracker.Service.Interfaces.Items;
public interface IItemService
{
    Task<bool> DeleteAsync(long id);
    Task<ItemViewModel> GetByIdAsync(long id);
    Task<IEnumerable<ItemViewModel>> GetAllAsync(PaginationParams @params);
    Task<ItemViewModel> CreateAsync(ItemPostModel model);
    Task<ItemViewModel> UpdateAsync(long id, ItemPostModel model);
}
