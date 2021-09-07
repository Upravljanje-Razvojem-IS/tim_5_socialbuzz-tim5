using AutoMapper;
using PorukaService.DTOs;
using PorukaService.Entities;

namespace PorukaService.MapperProfiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageReadDto>();
            CreateMap<Message, MessageConfirmationDto>();
        }
    }
}
