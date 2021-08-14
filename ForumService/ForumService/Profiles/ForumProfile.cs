using AutoMapper;
using ForumService.DTO;
using ForumService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Profiles
{
    public class ForumProfile : Profile
    {
        public ForumProfile() {
            CreateMap<Forum, ForumDTO>();
            CreateMap<ForumDTO, Forum>();
            CreateMap<Forum, Forum>();
        }
        
    }
}
