using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forum",
                columns: table => new
                {
                    ForumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ForumName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForumDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forum", x => x.ForumID);
                });

            migrationBuilder.CreateTable(
                name: "ForumMessage",
                columns: table => new
                {
                    ForumMessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ForumID = table.Column<int>(type: "int", nullable: false),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumMessage", x => x.ForumMessageID);
                    table.ForeignKey(
                        name: "FK_ForumMessage_Forum_ForumID",
                        column: x => x.ForumID,
                        principalTable: "Forum",
                        principalColumn: "ForumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Forum",
                columns: new[] { "ForumID", "ForumDescription", "ForumName", "IsOpen", "LogoLink", "UserID" },
                values: new object[] { 1, "This is forum about cars...", "Cars", true, "this is logo link", 1 });

            migrationBuilder.InsertData(
                table: "Forum",
                columns: new[] { "ForumID", "ForumDescription", "ForumName", "IsOpen", "LogoLink", "UserID" },
                values: new object[] { 2, "This is forum about toys...", "Toys", false, "this is logo link", 2 });

            migrationBuilder.InsertData(
                table: "ForumMessage",
                columns: new[] { "ForumMessageID", "Body", "ForumID", "SenderID", "Title" },
                values: new object[,]
                {
                    { 1, "My car is better than yours...", 1, 1, "This is first message in forum...." },
                    { 2, "No, my car is better than yours... I have lamborghini", 1, 3, "This is second message in forum...." },
                    { 11, "My toy is better than yours...", 2, 1, "This is first message in forum...." },
                    { 12, "No, my toy is better than yours... I have barbie", 2, 3, "This is second message in forum...." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumMessage_ForumID",
                table: "ForumMessage",
                column: "ForumID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForumMessage");

            migrationBuilder.DropTable(
                name: "Forum");
        }
    }
}
