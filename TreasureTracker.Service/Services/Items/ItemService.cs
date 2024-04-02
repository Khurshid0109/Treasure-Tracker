using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Service.Extentions;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Service.DTOs.Items;
using TreasureTracker.Service.Configurations;
using TreasureTracker.Service.Interfaces.Items;
using TreasureTracker.Service.Helpers.Exceptions;
using TreasureTracker.Service.Helpers.Media;

namespace TreasureTracker.Service.Services.Items;
public class ItemService : IItemService
{
    private readonly IMapper _mapper;
    private readonly IItemRepository _itemRepository;
    private readonly ICollectionRepository _collectionRepository;

    public ItemService(IMapper mapper,
                       IItemRepository itemRepository,
                       ICollectionRepository collectionRepository)
    {
        _mapper = mapper;
        _itemRepository = itemRepository;
        _collectionRepository = collectionRepository;
    }

    public async Task<ItemViewModel> CreateAsync(ItemPostModel model)
    {
       var collection = await _collectionRepository.GetAllAsync()
            .Where(c => c.Id == model.CollectionId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if(collection is null)
            throw new TTrackerException(404,"Collection is not found");

        var image = await MediaHelper.UploadFile(model.ImageUrl);

        var mapped = _mapper.Map<Item>(model);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.ImageUrl = image;

        var result = await _itemRepository.InsertAsync(mapped);
        await _itemRepository.SaveChangesAsync();

        return _mapper.Map<ItemViewModel>(result);
    }

    public async Task<bool> DeleteAsync(long id)
    {
       var item = await _itemRepository.GetAllAsync()
            .Where(i => i.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if(item is null)
            throw new TTrackerException(404,"Item is not found");

        await _itemRepository.DeleteAsync(id);
        await _itemRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<ItemViewModel>> GetAllAsync(PaginationParams @params)
    {
       var items = await _itemRepository.GetAllAsync()
            .ToPagedList<Item>(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ItemViewModel>>(items);
    }

    public async Task<ItemViewModel> GetByIdAsync(long id)
    {
        var item = await _itemRepository.GetAllAsync()
            .Where(i => i.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (item is null)
            throw new TTrackerException(404, "Item is not found");

        return _mapper.Map<ItemViewModel>(item);
    }

    public async Task<ItemViewModel> UpdateAsync(long id, ItemPostModel model)
    {
        var item = await _itemRepository.GetAllAsync()
            .Where(i => i.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (item is null)
            throw new TTrackerException(404, "Item is not found");

        var image = await MediaHelper.UploadFile(model.ImageUrl);

        var mapped = _mapper.Map(model, item);
        mapped.UpdatedAt = DateTime.UtcNow;
        mapped.ImageUrl=image;

        await _itemRepository.UpdateAasync(mapped);
        await _itemRepository.SaveChangesAsync();

        return _mapper.Map<ItemViewModel>(mapped);
    }
}
