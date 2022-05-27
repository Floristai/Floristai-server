using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Floristai.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Flowers_FlowerId",
                table: "OrderLines");

            migrationBuilder.DropIndex(
                name: "IX_OrderLines_FlowerId",
                table: "OrderLines");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_FlowerId",
                table: "OrderLines",
                column: "FlowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Flowers_FlowerId",
                table: "OrderLines",
                column: "FlowerId",
                principalTable: "Flowers",
                principalColumn: "FlowerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
