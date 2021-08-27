using AutoMapper;
using FriendsService.Dtos;
using FriendsService.Entities;

namespace FriendsService.Mappers
{
    public class FriendMapper : Profile
    {
        public FriendMapper()
        {
            CreateMap<Friend, FriendReadDto>();
            CreateMap<Friend, FriendWithUserDto>();
            CreateMap<FriendCreateDto, Friend>();
        }
    }
}
