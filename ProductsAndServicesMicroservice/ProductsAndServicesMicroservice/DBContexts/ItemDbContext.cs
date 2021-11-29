using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductsAndServicesMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.DBContexts
{
    public class ItemDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ItemDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PastPrice> PastPrices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DatabaseForProductsAndServices"));
        }

        /// <summary>
        /// Filling the database with some data
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new
                {
                    ItemId = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                    Name = "Lenovo TAB M10 FHD Plus",
                    Description = "Model - Lenovo TAB M10 FHD Plus, memorija - 4gb-128gb, polovan, perfektno ocuvan.",
                    Price = "39000.00 RSD",
                    AccountId = Guid.Parse("f2d8362a-124f-41a9-a22b-6e35b3a2953c"),
                    Weight = "1kg",
                    Quantity = "1",
                    ProductionDate = DateTime.UtcNow.AddDays(-10)
                },
                new
                {
                    ItemId = Guid.Parse("bee9273d-d6ed-47f8-84ed-b645dc6d9f2f"),
                    Name = "Wifi KAMERA poe kamera",
                    Description = "H\uFEFF265 i 4K visoka rezolucija, vodootporan, detekcija pokreta i nocni mod.",
                    Price = "11500.00 RSD",
                    AccountId = Guid.Parse("1bc6929f-0e75-4bef-a835-7dbb50d9e41a"),
                    Weight = "0.5kg",
                    Quantity = "3",
                    ProductionDate = DateTime.UtcNow.AddDays(-15)
                },
                new
                {
                    ItemId = Guid.Parse("c99d5b97-6984-43ef-b0a5-89d04569466e"),
                    Name = "iMac 27, 11.1 i7, 14gb,500hd",
                    Description = "﻿﻿﻿Konfiguracija\uFEFF I7 2.8ghz, 14gb rama, 500gb.",
                    Price = "45500.00 RSD",
                    AccountId = Guid.Parse("1bc6929f-0e75-4bef-a835-7dbb50d9e41a"),
                    Weight = "0.5kg",
                    Quantity = "2",
                    ProductionDate = DateTime.UtcNow.AddDays(-15)
                });

            modelBuilder.Entity<Service>().HasData(
               new
               {
                   ItemId = Guid.Parse("1f8aa5b3-a67f-45c5-b519-771a7c09a944"),
                   Name = "Popravka Sony i Panasonic maticnih ploca",
                   Description = "﻿﻿﻿Uspjesno\uFEFF rjesavamo probleme maticnih ploca Sony i Panasonic.",
                   Price = "8500.00 RSD",
                   AccountId = Guid.Parse("b1d1e043-85c9-4ee1-9eb7-38314c109607"),
                   StartDate = DateTime.UtcNow.AddDays(-10),
                   EndDate = DateTime.UtcNow.AddDays(-8)
               },
               new
               {
                   ItemId = Guid.Parse("2d53fc22-eac4-43bb-8f55-d2b8495603cc"),
                   Name = "Tretman lica- zlatna maska",
                   Description = "Zlatna maska je izuzetno kvalitetan proizvod . Na nevjerovatan nacin obnavlja kozu, hidrira je i zateze.",
                   Price = "2700.00 RSD",
                   AccountId = Guid.Parse("9888cf22-b353-4162-aedc-734ca2dc26a4"),
                   StartDate = DateTime.UtcNow.AddDays(-11),
                   EndDate = DateTime.UtcNow.AddDays(-6)
               });

            modelBuilder.Entity<PastPrice>().HasData(
                new
                {
                    PastPriceId = 1,
                    ItemId = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                    Price = "56998.00 RSD"
                },
                new
                {
                    PastPriceId = 2,
                    ItemId = Guid.Parse("bee9273d-d6ed-47f8-84ed-b645dc6d9f2f"),
                    Price = "17600.00 RSD"
                },
                new
                {
                    PastPriceId = 3,
                    ItemId = Guid.Parse("c99d5b97-6984-43ef-b0a5-89d04569466e"),
                    Price = "61200.00 RSD"
                },
                new
                {
                    PastPriceId = 4,
                    ItemId = Guid.Parse("1f8aa5b3-a67f-45c5-b519-771a7c09a944"),
                    Price = "6089.00 RSD"
                },
                new
                {
                    PastPriceId = 5,
                    ItemId = Guid.Parse("2d53fc22-eac4-43bb-8f55-d2b8495603cc"),
                    Price = "3050.00 RSD"
                });

        }
    }
}
