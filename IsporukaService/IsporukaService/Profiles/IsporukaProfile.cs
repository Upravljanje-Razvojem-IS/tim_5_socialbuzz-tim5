using AutoMapper;
using IsporukaService.DTOs.LokacijaDTOs;
using IsporukaService.Entities;

namespace IsporukaService.Profiles
{
    public class IsporukaProfile : Profile
    {
        public IsporukaProfile()
        {
            CreateMap<Lokacija, LokacijaReadDto>();
            CreateMap<Lokacija, LokacijaConfirmationDto>();
        }
    }
}
