﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductsAndServicesMicroservice.DBContexts;

namespace ProductsAndServicesMicroservice.Migrations
{
    [DbContext(typeof(ItemDbContext))]
    partial class ItemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductsAndServicesMicroservice.Entities.PastPrice", b =>
                {
                    b.Property<int>("PastPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PastPriceId");

                    b.ToTable("PastPrices");

                    b.HasData(
                        new
                        {
                            PastPriceId = 1,
                            ItemId = new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                            Price = "56998.00 RSD"
                        },
                        new
                        {
                            PastPriceId = 2,
                            ItemId = new Guid("bee9273d-d6ed-47f8-84ed-b645dc6d9f2f"),
                            Price = "17600.00 RSD"
                        },
                        new
                        {
                            PastPriceId = 3,
                            ItemId = new Guid("c99d5b97-6984-43ef-b0a5-89d04569466e"),
                            Price = "61200.00 RSD"
                        },
                        new
                        {
                            PastPriceId = 4,
                            ItemId = new Guid("1f8aa5b3-a67f-45c5-b519-771a7c09a944"),
                            Price = "6089.00 RSD"
                        },
                        new
                        {
                            PastPriceId = 5,
                            ItemId = new Guid("2d53fc22-eac4-43bb-8f55-d2b8495603cc"),
                            Price = "3050.00 RSD"
                        });
                });

            modelBuilder.Entity("ProductsAndServicesMicroservice.Entities.Product", b =>
                {
                    b.Property<Guid>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ProductionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Quantity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Weight")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ItemId = new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                            AccountId = new Guid("f2d8362a-124f-41a9-a22b-6e35b3a2953c"),
                            Description = "Model - Lenovo TAB M10 FHD Plus, memorija - 4gb-128gb, polovan, perfektno ocuvan.",
                            Name = "Lenovo TAB M10 FHD Plus",
                            Price = "39000.00 RSD",
                            ProductionDate = new DateTime(2021, 11, 18, 14, 20, 15, 214, DateTimeKind.Utc).AddTicks(7761),
                            Quantity = "1",
                            Weight = "1kg"
                        },
                        new
                        {
                            ItemId = new Guid("bee9273d-d6ed-47f8-84ed-b645dc6d9f2f"),
                            AccountId = new Guid("1bc6929f-0e75-4bef-a835-7dbb50d9e41a"),
                            Description = "﻿﻿﻿H.265 i 4K visoka rezolucija, vodootporan, detekcija pokreta i nocni mod.",
                            Name = "Wifi KAMERA poe kamera",
                            Price = "11500.00 RSD",
                            ProductionDate = new DateTime(2021, 11, 13, 14, 20, 15, 215, DateTimeKind.Utc).AddTicks(3516),
                            Quantity = "3",
                            Weight = "0.5kg"
                        },
                        new
                        {
                            ItemId = new Guid("c99d5b97-6984-43ef-b0a5-89d04569466e"),
                            AccountId = new Guid("1bc6929f-0e75-4bef-a835-7dbb50d9e41a"),
                            Description = "﻿﻿﻿Konfiguracija: I7 2.8ghz, 14gb rama, 500gb.",
                            Name = "iMac 27, 11.1 i7, 14gb,500hd",
                            Price = "45500.00 RSD",
                            ProductionDate = new DateTime(2021, 11, 13, 14, 20, 15, 215, DateTimeKind.Utc).AddTicks(3581),
                            Quantity = "2",
                            Weight = "0.5kg"
                        });
                });

            modelBuilder.Entity("ProductsAndServicesMicroservice.Entities.Service", b =>
                {
                    b.Property<Guid>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ItemId");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            ItemId = new Guid("1f8aa5b3-a67f-45c5-b519-771a7c09a944"),
                            AccountId = new Guid("b1d1e043-85c9-4ee1-9eb7-38314c109607"),
                            Description = "﻿﻿﻿Uspjesno rjesavamo probleme maticnih ploca Sony i Panasonic.",
                            EndDate = new DateTime(2021, 11, 20, 14, 20, 15, 218, DateTimeKind.Utc).AddTicks(2538),
                            Name = "Popravka Sony i Panasonic maticnih ploca",
                            Price = "8500.00 RSD",
                            StartDate = new DateTime(2021, 11, 18, 14, 20, 15, 218, DateTimeKind.Utc).AddTicks(2506)
                        },
                        new
                        {
                            ItemId = new Guid("2d53fc22-eac4-43bb-8f55-d2b8495603cc"),
                            AccountId = new Guid("9888cf22-b353-4162-aedc-734ca2dc26a4"),
                            Description = "Zlatna maska je izuzetno kvalitetan proizvod . Na nevjerovatan nacin obnavlja kozu, hidrira je i zateze.",
                            EndDate = new DateTime(2021, 11, 22, 14, 20, 15, 218, DateTimeKind.Utc).AddTicks(4324),
                            Name = "Tretman lica- zlatna maska",
                            Price = "2700.00 RSD",
                            StartDate = new DateTime(2021, 11, 17, 14, 20, 15, 218, DateTimeKind.Utc).AddTicks(4316)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}