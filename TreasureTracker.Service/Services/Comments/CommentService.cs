using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Service.Extentions;
using TreasureTracker.Data.IRepositories;
using TreasureTracker.Service.DTOs.Comments;
using TreasureTracker.Service.Configurations;
using TreasureTracker.Service.Helpers.Exceptions;
using TreasureTracker.Service.Interfaces.Comments;

namespace TreasureTracker.Service.Services.Comments;
public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository,
                          IMapper mapper,
                          IUserRepository userRepository,
                          IItemRepository itemRepository)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _itemRepository = itemRepository;
    }

    public async Task<CommentViewModel> CreateAsync(CommentPostModel model)
    {
        var user = await _userRepository.GetAllAsync()
            .Where(u => u.Id == model.UserId)
            .FirstOrDefaultAsync();

        if (user is null)
            throw new TTrackerException(404, "User is not found!");
       
        var item = await _itemRepository.GetAllAsync()
                         .Where(i => i.Id == model.ItemId)
                         .FirstOrDefaultAsync();

        if(item is null)
            throw new TTrackerException(404, "Item is not found!");

        var mapped = _mapper.Map<Comment>(model);
        mapped.CreatedAt = DateTime.Now;

        var result = await _commentRepository.InsertAsync(mapped);
        await _commentRepository.SaveChangesAsync();

        return _mapper.Map<CommentViewModel>(result);
    }

    public async Task<bool> DeleteAsync(long id)
    {
       var comment = await _commentRepository.GetAllAsync()
                                             .Where(c=>c.Id == id)
                                             .FirstOrDefaultAsync();
        if(comment is null)
            throw new TTrackerException(404, "Comment is not found!");

        await _commentRepository.DeleteAsync(id);
        await _commentRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<CommentViewModel>> GetAllAsync(PaginationParams @params)
    {
        var comments = await _commentRepository.GetAllAsync()
            .AsNoTracking()
            .ToPagedList<Comment>(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CommentViewModel>>(comments);
    }

    public async Task<CommentViewModel> GetByIdAsync(long id)
    {
       var comment = await _commentRepository.GetAllAsync()
                                             .Where(c=>c.Id == id)
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync();
        if(comment is null)
            throw new TTrackerException(404, "Comment is not found!");

        return _mapper.Map<CommentViewModel>(comment);
    }

    public async Task<CommentViewModel> UpdateAsync(long id, CommentPostModel model)
    {
        var comment = await _commentRepository.GetAllAsync()
                                             .Where(c=>c.Id == id)
                                             .FirstOrDefaultAsync();
        if(comment is null)
            throw new TTrackerException(404, "Comment is not found!");

        var user = await _userRepository.GetAllAsync()
           .Where(u => u.Id == model.UserId)
           .FirstOrDefaultAsync();

        if (user is null)
            throw new TTrackerException(404, "User is not found!");

        var item = await _itemRepository.GetAllAsync()
                         .Where(i => i.Id == model.ItemId)
                         .FirstOrDefaultAsync();

        if (item is null)
            throw new TTrackerException(404, "Item is not found!");

        var mapped = _mapper.Map(model, comment);
        mapped.UpdatedAt = DateTime.Now;

        await _commentRepository.UpdateAasync(mapped);
        await _commentRepository.SaveChangesAsync();

        return _mapper.Map<CommentViewModel>(mapped);
    }
}
