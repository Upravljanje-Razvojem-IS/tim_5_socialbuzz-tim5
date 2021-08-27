using AutoMapper;
using FriendsService.Dtos;
using FriendsService.Entities;

namespace FriendsService.Mappers
{
    public class LocationMapper : Profile
    {
        public LocationMapper()
        {
            CreateMap<Location, LocationReadDto>();
        }
    }
}
