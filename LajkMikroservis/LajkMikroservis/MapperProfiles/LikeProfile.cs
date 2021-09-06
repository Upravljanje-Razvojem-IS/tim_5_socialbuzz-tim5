using AutoMapper;
using LajkMikroservis.DTOs.LikeDTO;
using LajkMikroservis.Entities;

namespace LajkMikroservis.MapperProfiles
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<Like, LikeReadDto>();
            CreateMap<Like, LikeConfirmationDto>();
        }
    }
}
