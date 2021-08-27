using Microsoft.EntityFrameworkCore;
using BlockingService.Entities;
using BlockingService.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BlockingService.Repositories
{
    public class BlockRepository : IBlockRepository
    {
        private readonly AppDbContext _context;

        public BlockRepository(AppDbContext context)
        {
            _context = context;
        }

        public Block Create(Block entity)
        {
            _context.Blocks.Add(entity);

            _context.SaveChanges();

            return entity;
        }

        public IEnumerable<Block> Get()
        {
            return _context.Blocks;
        }

        public Block Get(int id)
        {
            return _context.Blocks.FirstOrDefault(e => e.Id == id);
        }

        public Block Update(Block entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();

            return entity;
        }

        public Block Delete(int id)
        {
            Block entity = _context.Blocks.FirstOrDefault(e => e.Id == id);

            if (entity != null)
            {
                _context.Blocks.Remove(entity);
                _context.SaveChanges();
            }

            return entity;
        }
    }
}
