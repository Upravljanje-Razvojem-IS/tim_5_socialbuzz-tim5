using CommentService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Data
{
    public class CommentContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public CommentContext(DbContextOptions<CommentContext> options) : base(options) { }
    }
}
