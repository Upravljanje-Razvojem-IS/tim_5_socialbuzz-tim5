using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectMessageService.Entity
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DataContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                 .HasData(new
                 {
                     MessageID = 1,
                     SenderID = 1,
                     ReceiverID = 2,
                     Body = "Cao ja sam Mila",
                     IsSent = true

                 },
                 new
                 {
                     MessageID = 2,
                     SenderID = 2,
                     ReceiverID = 1,
                     Body = "Cao ja sam Nemanja",
                     IsSent = true
                 }
                 );

           
        }
    }
}
