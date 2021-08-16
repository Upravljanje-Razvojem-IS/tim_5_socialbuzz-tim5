using Microsoft.EntityFrameworkCore.Migrations;

namespace DirectMessageService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    ReceiverID = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageID);
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "MessageID", "Body", "IsSent", "ReceiverID", "SenderID" },
                values: new object[] { 1, "Cao ja sam Mila", true, 2, 1 });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "MessageID", "Body", "IsSent", "ReceiverID", "SenderID" },
                values: new object[] { 2, "Cao ja sam Nemanja", true, 1, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
