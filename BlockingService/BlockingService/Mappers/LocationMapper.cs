using AutoMapper;
using BlockingService.Dtos;
using BlockingService.Entities;

namespace BlockingService.Mappers
{
    public class LocationMapper : Profile
    {
        public LocationMapper()
        {
            CreateMap<Location, LocationReadDto>();
        }
    }
}
