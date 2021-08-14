using AutoMapper;
using ForumService.DTO;
using ForumService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Profiles
{
    public class ForumMessageProfile : Profile
    {
        public ForumMessageProfile() {

            CreateMap<ForumMessage, ForumMessageDTO>();
            CreateMap<ForumMessageDTO, ForumMessage>();
            CreateMap<ForumMessage, ForumMessage>();
        }
    }
}
