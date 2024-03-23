using AutoMapper;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Service.DTOs.Users;

namespace TreasureTracker.Service.Mappers;
public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserPostModel>().ReverseMap();
        CreateMap<User,UserPutModel>().ReverseMap();
        CreateMap<User,UserViewModel>().ReverseMap();
    }
}
