using Microsoft.EntityFrameworkCore.Migrations;

namespace Web_Messaging_MVC.Migrations
{
    public partial class Init_Web : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessagingType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receiver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverName = table.Column<string>(nullable: true),
                    ReceiverEmail = table.Column<string>(nullable: true),
                    RecieverMobile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receiver", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderName = table.Column<string>(nullable: true),
                    SenderEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(nullable: false),
                    ReceiverId = table.Column<int>(nullable: false),
                    MessagingTypeId = table.Column<int>(nullable: false),
                    MessageBody = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_MessagingType_MessagingTypeId",
                        column: x => x.MessagingTypeId,
                        principalTable: "MessagingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_Receiver_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Receiver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_Sender_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Sender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_MessagingTypeId",
                table: "Message",
                column: "MessagingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReceiverId",
                table: "Message",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderId",
                table: "Message",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "MessagingType");

            migrationBuilder.DropTable(
                name: "Receiver");

            migrationBuilder.DropTable(
                name: "Sender");
        }
    }
}
