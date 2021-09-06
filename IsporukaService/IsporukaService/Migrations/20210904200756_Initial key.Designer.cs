﻿// <auto-generated />
using System;
using IsporukaService.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IsporukaService.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210904200756_Initial key")]
    partial class Initialkey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IsporukaService.Entities.Isporuka", b =>
                {
                    b.Property<int>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumIsporuke")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumPorudzbine")
                        .HasColumnType("datetime2");

                    b.Property<string>("Firma")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KupacId")
                        .HasColumnType("int");

                    b.Property<Guid>("LokacijaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProdavacId")
                        .HasColumnType("int");

                    b.Property<decimal>("Trosak")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Guid");

                    b.HasIndex("LokacijaId");

                    b.ToTable("Isporuke");
                });

            modelBuilder.Entity("IsporukaService.Entities.Lokacija", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Drzava")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ptt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lokacije");
                });

            modelBuilder.Entity("IsporukaService.Entities.Isporuka", b =>
                {
                    b.HasOne("IsporukaService.Entities.Lokacija", "Lokacija")
                        .WithMany("Isporuke")
                        .HasForeignKey("LokacijaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lokacija");
                });

            modelBuilder.Entity("IsporukaService.Entities.Lokacija", b =>
                {
                    b.Navigation("Isporuke");
                });
#pragma warning restore 612, 618
        }
    }
}
