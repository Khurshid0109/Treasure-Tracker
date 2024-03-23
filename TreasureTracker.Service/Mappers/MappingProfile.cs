using AutoMapper;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Service.DTOs.Users;

namespace TreasureTracker.Service.Mappers;
public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<UserPostModel, User>().ReverseMap();
        CreateMap<UserPutModel,User>().ReverseMap();
        CreateMap<UserViewModel, User>().ReverseMap();
    }
}
