using Microsoft.EntityFrameworkCore;
using PorukaService.Entities;

namespace PorukaService.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public virtual DbSet<Message> Messages { get; set; }
    }
}
