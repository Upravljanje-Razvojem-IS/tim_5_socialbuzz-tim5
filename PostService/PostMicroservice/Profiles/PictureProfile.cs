using AutoMapper;
using PostMicroservice.Entities;
using PostMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Profiles
{
    public class PictureProfile : Profile
    {

        public PictureProfile()
        {
         

            CreateMap<PictureDto,Picture>();
            CreateMap<PictureCreationDto, Picture>();
            CreateMap<Picture, PictureDto>();

            CreateMap<PictureUpdateDto, Picture>();
            CreateMap<Picture, PictureConfirmationDto>();
            CreateMap<Picture, Picture>();

        }
    }
}
