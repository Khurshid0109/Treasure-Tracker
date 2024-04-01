using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Service.Configurations;
using TreasureTracker.Service.Helpers.Exceptions;
using TreasureTracker.Service.DTOs.UserCodes;
using TreasureTracker.Service.Extentions;
using TreasureTracker.Service.Interfaces.UserCodes;

namespace TreasureTracker.Service.Services.UserCodes;
public class UserCodeService:IUserCodeService
{
    private readonly IMapper _mapper;
    private readonly IUserCodeRepository _repository;
    private readonly IUserRepository _userRepository;

    public UserCodeService(IUserCodeRepository repository,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserCodeViewModel> CreateAsync(UserCode model)
    {
        var user = await _userRepository.GetAllAsync()
            .IgnoreQueryFilters()
            .Where(u => u.Id == model.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is null)
            throw new TTrackerException(404, "User is not found!");

        model.ExpireDate = DateTime.UtcNow.AddMinutes(3);
        model.CreatedAt = DateTime.UtcNow;

        var result = await _repository.InsertAsync(model);
        await _repository.SaveChangesAsync();

        return _mapper.Map<UserCodeViewModel>(result);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var code = await _repository.GetAllAsync()
             .Where(c => c.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (code is null)
            throw new TTrackerException(404, "Code is not found!");

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<UserCodeViewModel>> GetAllAsync(PaginationParams @params)
    {
        var codes = await _repository.GetAllAsync()
             .ToPagedList<UserCode>(@params)
             .ToListAsync();

        return _mapper.Map<IEnumerable<UserCodeViewModel>>(codes);
    }

    public async Task<UserCodeViewModel> GetByIdAsync(long id)
    {
        var code = await _repository.GetAllAsync()
             .Where(c => c.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (code is null)
            throw new TTrackerException(404, "Code is not found!");

        return _mapper.Map<UserCodeViewModel>(code);
    }
}

