using Microsoft.EntityFrameworkCore.Migrations;

namespace WidgetServer.Migrations
{
    public partial class ForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Widget",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Widget_UserId",
                table: "Widget",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Widget_User_UserId",
                table: "Widget",
                column: "UserId",
                principalTable: "User",
                principalColumn: "EmailAddress",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widget_User_UserId",
                table: "Widget");

            migrationBuilder.DropIndex(
                name: "IX_Widget_UserId",
                table: "Widget");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Widget");
        }
    }
}
