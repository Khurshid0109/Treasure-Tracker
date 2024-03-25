using TreasureTracker.Service.Configurations;
using TreasureTracker.Service.DTOs.Categories;

namespace TreasureTracker.Service.Interfaces.Categories;
public interface ICategoryService
{
    Task<CategoryViewModel> GetByIdAsync(long id);
    Task<IEnumerable<CategoryViewModel>> GetAllAsync(PaginationParams @params);
    Task<CategoryViewModel> CreateAsync(CategoryPostModel model);
    Task<CategoryViewModel> UpdateAsync(long id, CategoryPostModel model);
}
