using Microsoft.EntityFrameworkCore;
using BlockingService.Entities;
using BlockingService.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BlockingService.Repositories
{
    public class BlockListRepository : IBlockListRepository
    {
        private readonly AppDbContext _context;

        public BlockListRepository(AppDbContext context)
        {
            _context = context;
        }

        public BlockList Create(BlockList entity)
        {
            _context.BlockLists.Add(entity);

            _context.SaveChanges();

            return entity;
        }

        public IEnumerable<BlockList> Get()
        {
            return _context.BlockLists.Include(e => e.Blocks);
        }

        public BlockList Get(int id)
        {
            return _context.BlockLists.Include(e => e.Blocks).FirstOrDefault(e => e.Id == id);
        }

        public BlockList Update(BlockList entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();

            return entity;
        }

        public BlockList Delete(int id)
        {
            BlockList entity = _context.BlockLists.FirstOrDefault(e => e.Id == id);

            if (entity != null)
            {
                _context.BlockLists.Remove(entity);
                _context.SaveChanges();
            }

            return entity;
        }
    }
}
