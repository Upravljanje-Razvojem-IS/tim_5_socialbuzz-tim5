using Microsoft.EntityFrameworkCore;

namespace BlockingService.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Block> Blocks { get; set; }
        public DbSet<BlockList> BlockLists { get; set; }
    }
}
