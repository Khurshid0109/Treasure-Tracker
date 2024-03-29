﻿using AutoMapper;
using TreasureTracker.Domain.Entities;
using TreasureTracker.Service.DTOs.Categories;
using TreasureTracker.Service.DTOs.Collections;
using TreasureTracker.Service.DTOs.Comments;
using TreasureTracker.Service.DTOs.Messages;
using TreasureTracker.Service.DTOs.UserCodes;
using TreasureTracker.Service.DTOs.Users;
using TreasureTracker.Service.Helpers;

namespace TreasureTracker.Service.Mappers;
public class MappingProfile:Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserPostModel>().ReverseMap();
        CreateMap<User,UserPutModel>().ReverseMap();
        CreateMap<User,UserViewModel>().ReverseMap();

        // UserCode
        CreateMap<UserCode,UserCodePostModel>().ReverseMap();
        CreateMap<UserCode,UserCodeViewModel>().ReverseMap();

        // Message
        CreateMap<Message,MessagePostModel>().ReverseMap();
        CreateMap<Message,MessageViewModel>().ReverseMap();

        // Category
        CreateMap<Category,CategoryPostModel>().ReverseMap();
        CreateMap<Category,CategoryViewModel>().ReverseMap();

        // Comment
        CreateMap<Comment,CommentPostModel>().ReverseMap();
        CreateMap<Comment,CommentViewModel>().ReverseMap();

        // Collection
        CreateMap<Collection,CollectionPostModel>().ReverseMap();
        CreateMap<Collection,CollectionViewModel>().ReverseMap();
    }
}
