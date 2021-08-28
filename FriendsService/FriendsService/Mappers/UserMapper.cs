using AutoMapper;
using FriendsService.Dtos;
using FriendsService.Entities;

namespace FriendsService.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserReadDto>();
        }
    }
}
