using BlockingService.Entities;
using System.Collections.Generic;

namespace BlockingService.Interfaces
{
    public interface IBlockRepository
    {
        Block Create(Block entity);
        IEnumerable<Block> Get();
        Block Get(int id);
        Block Update(Block entity);
        Block Delete(int id);
    }
}
