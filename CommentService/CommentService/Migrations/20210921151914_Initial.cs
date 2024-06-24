using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommentService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(90)", nullable: true),
                    TimeAndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");
        }
    }
}
