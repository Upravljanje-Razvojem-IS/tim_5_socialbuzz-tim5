using AutoMapper;
using ProductsAndServicesMicroservice.DTOs;
using ProductsAndServicesMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ItemDto>();

            CreateMap<Service, Item>();
            CreateMap<ServiceCreationDto, Service>();
            CreateMap<Service, ServiceConfirmationDto>();

            CreateMap<ServiceUpdateDto, Service>();
        }
    }
}
