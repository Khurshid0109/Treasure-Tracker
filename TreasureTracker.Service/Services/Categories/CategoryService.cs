using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Service.Extentions;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Service.Configurations;
using TreasureTracker.Service.DTOs.Categories;
using TreasureTracker.Service.Helpers.Exceptions;
using TreasureTracker.Service.Interfaces.Categories;

namespace TreasureTracker.Service.Services.Categories;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryViewModel> CreateAsync(CategoryPostModel model)
    {
        var category = await _categoryRepository.GetAllAsync()
             .Where(c => c.Name.ToLower() == model.Name.ToLower())
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if(category is not null)
            throw new TTrackerException(400,"Category is already exist");

        var mapped = _mapper.Map<Category>(model);
        mapped.CreatedAt = DateTime.UtcNow;

        var result = await _categoryRepository.InsertAsync(mapped);
        await _categoryRepository.SaveChangesAsync();

        return _mapper.Map<CategoryViewModel>(result);
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllAsync(PaginationParams @params)
    {
        var categories = await _categoryRepository.GetAllAsync()
             .ToPagedList<Category>(@params)
             .ToListAsync();

        return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
    }

    public async Task<CategoryViewModel> GetByIdAsync(long id)
    {
        var category = await _categoryRepository.GetAllAsync()
            .Where(c => c.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if(category is null)
            throw new TTrackerException(404,"Category is not found");

        return _mapper.Map<CategoryViewModel>(category);
    }

    public async Task<CategoryViewModel> UpdateAsync(long id, CategoryPostModel model)
    {
        var category = await _categoryRepository.GetAllAsync()
            .Where(c => c.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (category is null)
            throw new TTrackerException(404, "Category is not found!");

        var mapped = _mapper.Map(model, category);
        mapped.UpdatedAt = DateTime.Now;

        await _categoryRepository.UpdateAasync(mapped);
        await _categoryRepository.SaveChangesAsync();

        return _mapper.Map<CategoryViewModel>(mapped);
    }
}
