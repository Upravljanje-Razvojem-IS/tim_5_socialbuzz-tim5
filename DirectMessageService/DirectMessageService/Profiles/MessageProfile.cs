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
            CreateMap<Message, Message>();
            CreateMap<MessageDTO, Message>();
        }
    }
}
