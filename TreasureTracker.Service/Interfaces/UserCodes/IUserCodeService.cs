using TreasureTracker.Domain.Entities;
using TreasureTracker.Service.Configurations;
using TreasureTracker.Service.DTOs.UserCodes;

namespace TreasureTracker.Service.Interfaces.UserCodes;
public interface IUserCodeService
{
    Task<bool> DeleteAsync(long id);
    Task<UserCodeViewModel> GetByIdAsync(long id);
    Task<UserCodeViewModel> CreateAsync(UserCode model);
    Task<IEnumerable<UserCodeViewModel>> GetAllAsync(PaginationParams @params);
}
