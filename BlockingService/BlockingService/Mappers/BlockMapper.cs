using AutoMapper;
using BlockingService.Dtos;
using BlockingService.Entities;

namespace BlockingService.Mappers
{
    public class BlockMapper : Profile
    {
        public BlockMapper()
        {
            CreateMap<Block, BlockReadDto>();
            CreateMap<Block, BlockWithUserDto>();
            CreateMap<BlockCreateDto, Block>();
        }
    }
}
