using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DirectMessageService.DTO;
using DirectMessageService.Entity;

namespace DirectMessageService.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDTO>();
            CreateMap<Message, MessageCreateDTO>();
            CreateMap<MessageCreateDTO, Message>();
            CreateMap<Message, Message>();
            CreateMap<MessageDTO, MessageCreateDTO>();
            CreateMap<MessageCreateDTO, MessageDTO>();
            CreateMap<MessageDTO, Message>();
        }
    }
}
