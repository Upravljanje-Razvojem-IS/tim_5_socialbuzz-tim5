using AutoMapper;
using IsporukaService.DTOs.LokacijaDTOs;
using IsporukaService.Entities;

namespace IsporukaService.Profiles
{
    public class LokacijaProfile : Profile
    {
        public LokacijaProfile()
        {
            CreateMap<Lokacija, LokacijaCreateDto>();
            CreateMap<Lokacija, LokacijaConfirmationDto>();
        }
    }
}
