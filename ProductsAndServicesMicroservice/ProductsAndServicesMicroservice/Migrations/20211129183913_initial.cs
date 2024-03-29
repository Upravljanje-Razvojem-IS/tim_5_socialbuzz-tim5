﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAndServicesMicroservice.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PastPrices",
                columns: table => new
                {
                    PastPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastPrices", x => x.PastPriceId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ItemId);
                });

            migrationBuilder.InsertData(
                table: "PastPrices",
                columns: new[] { "PastPriceId", "ItemId", "Price" },
                values: new object[,]
                {
                    { 1, new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"), "56998.00 RSD" },
                    { 2, new Guid("bee9273d-d6ed-47f8-84ed-b645dc6d9f2f"), "17600.00 RSD" },
                    { 3, new Guid("c99d5b97-6984-43ef-b0a5-89d04569466e"), "61200.00 RSD" },
                    { 4, new Guid("1f8aa5b3-a67f-45c5-b519-771a7c09a944"), "6089.00 RSD" },
                    { 5, new Guid("2d53fc22-eac4-43bb-8f55-d2b8495603cc"), "3050.00 RSD" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ItemId", "AccountId", "Description", "Name", "Price", "ProductionDate", "Quantity", "Weight" },
                values: new object[,]
                {
                    { new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"), new Guid("f2d8362a-124f-41a9-a22b-6e35b3a2953c"), "Model - Lenovo TAB M10 FHD Plus, memorija - 4gb-128gb, polovan, perfektno ocuvan.", "Lenovo TAB M10 FHD Plus", "39000.00 RSD", new DateTime(2021, 11, 19, 18, 39, 11, 890, DateTimeKind.Utc).AddTicks(6052), "1", "1kg" },
                    { new Guid("bee9273d-d6ed-47f8-84ed-b645dc6d9f2f"), new Guid("1bc6929f-0e75-4bef-a835-7dbb50d9e41a"), "H﻿265 i 4K visoka rezolucija, vodootporan, detekcija pokreta i nocni mod.", "Wifi KAMERA poe kamera", "11500.00 RSD", new DateTime(2021, 11, 14, 18, 39, 11, 891, DateTimeKind.Utc).AddTicks(1592), "3", "0.5kg" },
                    { new Guid("c99d5b97-6984-43ef-b0a5-89d04569466e"), new Guid("1bc6929f-0e75-4bef-a835-7dbb50d9e41a"), "﻿﻿﻿Konfiguracija﻿ I7 2.8ghz, 14gb rama, 500gb.", "iMac 27, 11.1 i7, 14gb,500hd", "45500.00 RSD", new DateTime(2021, 11, 14, 18, 39, 11, 891, DateTimeKind.Utc).AddTicks(1657), "2", "0.5kg" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ItemId", "AccountId", "Description", "EndDate", "Name", "Price", "StartDate" },
                values: new object[,]
                {
                    { new Guid("1f8aa5b3-a67f-45c5-b519-771a7c09a944"), new Guid("b1d1e043-85c9-4ee1-9eb7-38314c109607"), "﻿﻿﻿Uspjesno﻿ rjesavamo probleme maticnih ploca Sony i Panasonic.", new DateTime(2021, 11, 21, 18, 39, 11, 893, DateTimeKind.Utc).AddTicks(189), "Popravka Sony i Panasonic maticnih ploca", "8500.00 RSD", new DateTime(2021, 11, 19, 18, 39, 11, 893, DateTimeKind.Utc).AddTicks(173) },
                    { new Guid("2d53fc22-eac4-43bb-8f55-d2b8495603cc"), new Guid("9888cf22-b353-4162-aedc-734ca2dc26a4"), "Zlatna maska je izuzetno kvalitetan proizvod . Na nevjerovatan nacin obnavlja kozu, hidrira je i zateze.", new DateTime(2021, 11, 23, 18, 39, 11, 893, DateTimeKind.Utc).AddTicks(1852), "Tretman lica- zlatna maska", "2700.00 RSD", new DateTime(2021, 11, 18, 18, 39, 11, 893, DateTimeKind.Utc).AddTicks(1844) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PastPrices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
