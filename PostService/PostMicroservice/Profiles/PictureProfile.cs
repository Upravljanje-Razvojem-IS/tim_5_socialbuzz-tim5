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
         

            CreateMap<PictureDTO,Picture>();
            CreateMap<PictureCreationDTO, Picture>();
            CreateMap<Picture, PictureDTO>();

            CreateMap<PictureUpdateDTO, Picture>();
            CreateMap<Picture, PictureConfirmationDTO>();
        }
    }
}
