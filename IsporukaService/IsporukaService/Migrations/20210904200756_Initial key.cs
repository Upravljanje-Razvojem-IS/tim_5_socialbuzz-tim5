using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsporukaService.Migrations
{
    public partial class Initialkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lokacije",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ptt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Isporuke",
                columns: table => new
                {
                    Guid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumPorudzbine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumIsporuke = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Firma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trosak = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProdavacId = table.Column<int>(type: "int", nullable: false),
                    KupacId = table.Column<int>(type: "int", nullable: false),
                    LokacijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Isporuke", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Isporuke_Lokacije_LokacijaId",
                        column: x => x.LokacijaId,
                        principalTable: "Lokacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Isporuke_LokacijaId",
                table: "Isporuke",
                column: "LokacijaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Isporuke");

            migrationBuilder.DropTable(
                name: "Lokacije");
        }
    }
}
