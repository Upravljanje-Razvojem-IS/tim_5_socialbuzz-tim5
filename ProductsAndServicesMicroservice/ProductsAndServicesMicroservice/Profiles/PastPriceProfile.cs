using AutoMapper;
using ProductsAndServicesMicroservice.DTOs;
using ProductsAndServicesMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Profiles
{
    public class PastPriceProfile : Profile
    {
        public PastPriceProfile()
        {
            CreateMap<PastPriceDto, PastPrice>();
            CreateMap<PastPriceCreationDto, PastPrice>();
        }
    }
}
