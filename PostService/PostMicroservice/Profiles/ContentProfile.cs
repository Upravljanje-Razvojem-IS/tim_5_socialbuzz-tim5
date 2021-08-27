using AutoMapper;
using PostMicroservice.Entities;
using PostMicroservice.Models;
using PostMicroservice.Models.ContentDto;
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


            CreateMap<ContentDto, Content>();
            CreateMap<ContentCreationDto, Content>();
            CreateMap<Content, ContentDto>();

            CreateMap<ContentUpdateDto, Content>();
            CreateMap<Content, ContentConfirmationDto>();
            CreateMap<Content, Content>();

        }
    }
}
