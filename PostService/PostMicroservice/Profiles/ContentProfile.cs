using AutoMapper;
using PostMicroservice.Entities;
using PostMicroservice.Models;
using PostMicroservice.Models.ContentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Profiles
{
    public class ContentProfile : Profile
    {

        public ContentProfile()
        {


            CreateMap<ContentDTO, Content>();
            CreateMap<ContentCreationDTO, Content>();
            CreateMap<Content, ContentDTO>();

            CreateMap<ContentUpdateDTO, Content>();
            CreateMap<Content, ContentConfirmationDTO>();
            CreateMap<Content, Content>();

        }
    }
}
