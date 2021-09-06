using LajkMikroservis.Entities;
using Microsoft.EntityFrameworkCore;

namespace LajkMikroservis.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<LikeTip> LikeTipovi { get; set; }
    }
}
