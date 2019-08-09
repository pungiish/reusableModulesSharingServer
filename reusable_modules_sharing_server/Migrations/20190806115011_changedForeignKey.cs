using Microsoft.EntityFrameworkCore.Migrations;

namespace WidgetServer.Migrations
{
    public partial class changedForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widget_User_UserId",
                table: "Widget");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Widget",
                newName: "userEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Widget_UserId",
                table: "Widget",
                newName: "IX_Widget_userEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Widget_User_userEmail",
                table: "Widget",
                column: "userEmail",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widget_User_userEmail",
                table: "Widget");

            migrationBuilder.RenameColumn(
                name: "userEmail",
                table: "Widget",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Widget_userEmail",
                table: "Widget",
                newName: "IX_Widget_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Widget_User_UserId",
                table: "Widget",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
