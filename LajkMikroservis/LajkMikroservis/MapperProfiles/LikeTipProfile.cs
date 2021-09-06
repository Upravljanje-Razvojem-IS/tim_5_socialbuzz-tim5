using AutoMapper;
using LajkMikroservis.DTOs.LikeTipDTO;
using LajkMikroservis.Entities;

namespace LajkMikroservis.MapperProfiles
{
    public class LikeTipProfile : Profile
    {
        public LikeTipProfile()
        {
            CreateMap<LikeTip, LikeTipReadDto>();
            CreateMap<LikeTip, LikeTipConfirmationDto>();
        }
    }
}
