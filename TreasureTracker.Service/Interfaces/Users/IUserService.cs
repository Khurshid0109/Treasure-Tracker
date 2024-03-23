using TreasureTracker.Service.Configurations;
using TreasureTracker.Service.DTOs.Users;

namespace TreasureTracker.Service.Interfaces.Users;
public interface IUserService
{
    Task<bool> DeleteAsync(long id);
    Task<UserViewModel> GetByIdAsync(long id);
    Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params);
    Task<UserViewModel> GetByEmailAsync(string email);
    Task<UserViewModel> CreateAsync(UserPostModel model);
    Task<UserViewModel> UpdateAsync(long id, UserPostModel model);
}
