using Microsoft.EntityFrameworkCore.Migrations;

namespace WidgetServer.Migrations
{
    public partial class RemovedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widget_User_Username",
                table: "Widget");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Widget");

            migrationBuilder.RenameColumn(
                name: "Widget",
                table: "Widget",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Widget",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Widget_UserId",
                table: "Widget",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Widget_User_UserId",
                table: "Widget",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Email",
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

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Widget",
                newName: "Widget");

            migrationBuilder.AlterColumn<string>(
                name: "Widget",
                table: "Widget",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Widget",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Widget_User_Username",
                table: "Widget",
                column: "Username",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
