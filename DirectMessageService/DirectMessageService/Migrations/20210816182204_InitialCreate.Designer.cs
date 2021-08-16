﻿// <auto-generated />
using DirectMessageService.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DirectMessageService.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210816182204_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DirectMessageService.Entity.Message", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSent")
                        .HasColumnType("bit");

                    b.Property<int>("ReceiverID")
                        .HasColumnType("int");

                    b.Property<int>("SenderID")
                        .HasColumnType("int");

                    b.HasKey("MessageID");

                    b.ToTable("Messages");

                    b.HasData(
                        new
                        {
                            MessageID = 1,
                            Body = "Cao ja sam Mila",
                            IsSent = true,
                            ReceiverID = 2,
                            SenderID = 1
                        },
                        new
                        {
                            MessageID = 2,
                            Body = "Cao ja sam Nemanja",
                            IsSent = true,
                            ReceiverID = 1,
                            SenderID = 2
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
