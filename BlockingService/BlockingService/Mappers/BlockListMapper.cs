using AutoMapper;
using BlockingService.Dtos;
using BlockingService.Entities;

namespace BlockingService.Mappers
{
    public class BlockListMapper : Profile
    {
        public BlockListMapper()
        {
            CreateMap<BlockList, BlockListReadDto>();
            CreateMap<BlockListCreateDto, BlockList>();
        }
    }
}
