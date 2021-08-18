using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Entity
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DataContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Forum> Forums { get; set; }
        public DbSet<ForumMessage> ForumMessages { get; set; }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Forum>()
                 .HasData(new
                 {
                     ForumID = 1,
                     ForumName = "Cars",
                     ForumDescription = "This is forum about cars...",
                     UserID = 1,
                     LogoLink = "this is logo link",
                     IsOpen = true

                 },
                 new
                 {
                     ForumID = 2,
                     ForumName = "Toys",
                     ForumDescription = "This is forum about toys...",
                     UserID = 2,
                     LogoLink = "this is logo link",
                     IsOpen = false

                 }
                 );

            modelBuilder.Entity<ForumMessage>()
                .HasData(new
                {
                    ForumMessageID = 1,
                    ForumID = 1,
                    SenderID = 1,
                    Title = "This is first message in forum....",
                    Body = "My car is better than yours..."

                },
                new
                {
                    ForumMessageID = 2,
                    ForumID = 1,
                    SenderID = 3,
                    Title = "This is second message in forum....",
                    Body = "No, my car is better than yours... I have lamborghini"

                },
                new
                {
                    ForumMessageID = 11,
                    ForumID = 2,
                    SenderID = 1,
                    Title = "This is first message in forum....",
                    Body = "My toy is better than yours..."

                },
                new
                {
                    ForumMessageID = 12,
                    ForumID = 2,
                    SenderID = 3,
                    Title = "This is second message in forum....",
                    Body = "No, my toy is better than yours... I have barbie"

                }
                );
        }
    }
}
