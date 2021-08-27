using AutoMapper;
using PostMicroservice.Entities;
using PostMicroservice.Models.PostDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Profiles
{
    public class PostProfile : Profile
    {

        public PostProfile()
        {


            CreateMap<PostDto, Post>();
            CreateMap<PostCreationDto, Post>();
            CreateMap<Post, PostDto>();

            CreateMap<PostUpdateDto, Post>();
            CreateMap<Post, PostConfirmationDto>();
            CreateMap<Post, Post>();

        }
    }
}
