using AutoMapper;
using PostMicroservice.Entities;
using PostMicroservice.Models.PostDTO;
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


            CreateMap<PostDTO, Post>();
            CreateMap<PostCreationDTO, Post>();
            CreateMap<Post, PostDTO>();

            CreateMap<PostUpdateDTO, Post>();
            CreateMap<Post, PostConfirmationDTO>();
            CreateMap<Post, Post>();

        }
    }
}
