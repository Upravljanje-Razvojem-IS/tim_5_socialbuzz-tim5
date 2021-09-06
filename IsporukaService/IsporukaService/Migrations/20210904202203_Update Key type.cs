using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsporukaService.Migrations
{
    public partial class UpdateKeytype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Isporuke",
                table: "Isporuke");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Isporuke");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Isporuke",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Isporuke",
                table: "Isporuke",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Isporuke",
                table: "Isporuke");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Isporuke");

            migrationBuilder.AddColumn<int>(
                name: "Guid",
                table: "Isporuke",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Isporuke",
                table: "Isporuke",
                column: "Guid");
        }
    }
}
