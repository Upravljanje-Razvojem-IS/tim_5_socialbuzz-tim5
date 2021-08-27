using Microsoft.EntityFrameworkCore;

namespace FriendsService.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Friend> Friends { get; set; }
        public DbSet<FriendList> FriendLists { get; set; }
    }
}
