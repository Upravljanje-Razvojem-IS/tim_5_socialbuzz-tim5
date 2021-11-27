using ProductsAndServicesMicroservice.DTOs;
using ProductsAndServicesMicroservice.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ItemDto>();

            CreateMap<Product, Item>();
            CreateMap<ProductCreationDto, Product>();
            CreateMap<Product, ProductConfirmationDto>();

            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
