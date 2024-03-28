using AutoMapper;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Service.Configurations;
using TreasureTracker.Service.DTOs.Collections;
using TreasureTracker.Service.Helpers.Exceptions;
using TreasureTracker.Service.Helpers.Media;
using TreasureTracker.Service.Interfaces.Categories;
using TreasureTracker.Service.Interfaces.Collections;
using TreasureTracker.Service.Interfaces.Users;

namespace TreasureTracker.Service.Services.Collections;
public class CollectionService : ICollectionService
{
    private readonly IUserService _userService;
    private readonly ICategoryService _categoryService;
    private readonly ICollectionRepository _collectionRepository;
    private readonly IMapper _mapper;

    public CollectionService(IUserService userService,
                             ICategoryService categoryService,
                             IMapper mapper,
                             ICollectionRepository collectionRepository)
    {
        _userService = userService;
        _categoryService = categoryService;
        _mapper = mapper;
        _collectionRepository = collectionRepository;
    }

    public async Task<CollectionViewModel> CreateAsync(CollectionPostModel model)
    {
       var user = await _userService.GetByIdAsync(model.UserId);

        if (user is null)
            throw new TTrackerException(404, "User is not found!");

        var category = await _categoryService.GetByIdAsync(model.CategoryId);

        if(category is null)
            throw new TTrackerException(404, "Category is not found!");

        var image = await MediaHelper.UploadFile(model.ImageUrl);

        var mapped = _mapper.Map<Collection>(model);
        mapped.CreatedAt=DateTime.UtcNow;
        mapped.ImageUrl = image;

        var result = await _collectionRepository.InsertAsync(mapped);
        await _collectionRepository.SaveChangesAsync();

        return _mapper.Map<CollectionViewModel>(result);
    }

    public async Task<bool> DeleteAsync(long id)
    {
      var collection = await _collectionRepository.GetByIdAsync(id);

        if(collection is null)
            throw new TTrackerException(404, "Collection is not found!");

        await _collectionRepository.DeleteAsync(id);
        await _collectionRepository.SaveChangesAsync();

        return true;
    }

    public Task<IEnumerable<CollectionViewModel>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<CollectionViewModel> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<CollectionViewModel> UpdateAsync(long id, CollectionPostModel model)
    {
        throw new NotImplementedException();
    }
}
