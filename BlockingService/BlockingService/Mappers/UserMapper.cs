using AutoMapper;
using BlockingService.Dtos;
using BlockingService.Entities;

namespace BlockingService.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserReadDto>();
        }
    }
}
