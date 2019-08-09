using Microsoft.EntityFrameworkCore.Migrations;

namespace WidgetServer.Migrations
{
    public partial class RemovedUserEmai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widget_User_userEmail",
                table: "Widget");

            migrationBuilder.RenameColumn(
                name: "userEmail",
                table: "Widget",
                newName: "Widget");

            migrationBuilder.RenameIndex(
                name: "IX_Widget_userEmail",
                table: "Widget",
                newName: "IX_Widget_Widget");

            migrationBuilder.AddForeignKey(
                name: "FK_Widget_User_Widget",
                table: "Widget",
                column: "Widget",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widget_User_Widget",
                table: "Widget");

            migrationBuilder.RenameColumn(
                name: "Widget",
                table: "Widget",
                newName: "userEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Widget_Widget",
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
    }
}
