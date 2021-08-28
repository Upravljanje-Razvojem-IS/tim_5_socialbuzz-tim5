using BlockingService.Entities;
using System.Collections.Generic;

namespace BlockingService.Interfaces
{
    public interface IBlockListRepository
    {
        BlockList Create(BlockList entity);
        IEnumerable<BlockList> Get();
        BlockList Get(int id);
        BlockList Update(BlockList entity);
        BlockList Delete(int id);
    }
}
