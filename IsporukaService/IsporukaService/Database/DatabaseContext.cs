using IsporukaService.Entities;
using Microsoft.EntityFrameworkCore;

namespace IsporukaService.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public virtual DbSet<Isporuka> Isporuke { get; set; }
        public virtual DbSet<Lokacija> Lokacije { get; set; }
    }
}
