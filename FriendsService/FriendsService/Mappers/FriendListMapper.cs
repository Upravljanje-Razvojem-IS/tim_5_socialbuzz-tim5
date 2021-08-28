using AutoMapper;
using FriendsService.Dtos;
using FriendsService.Entities;

namespace FriendsService.Mappers
{
    public class FriendListMapper : Profile
    {
        public FriendListMapper()
        {
            CreateMap<FriendList, FriendListReadDto>();
            CreateMap<FriendListCreateDto, FriendList>();
        }
    }
}
