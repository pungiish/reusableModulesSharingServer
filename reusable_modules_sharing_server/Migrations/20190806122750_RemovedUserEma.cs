using Microsoft.EntityFrameworkCore.Migrations;

namespace WidgetServer.Migrations
{
    public partial class RemovedUserEma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widget_User_Widget",
                table: "Widget");

            migrationBuilder.RenameColumn(
                name: "Widget",
                table: "Widget",
                newName: "Widgets");

            migrationBuilder.RenameIndex(
                name: "IX_Widget_Widget",
                table: "Widget",
                newName: "IX_Widget_Widgets");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Widget",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Widget_Username",
                table: "Widget",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Widget_User_Username",
                table: "Widget",
                column: "Username",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Widget_User_Widgets",
                table: "Widget",
                column: "Widgets",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widget_User_Username",
                table: "Widget");

            migrationBuilder.DropForeignKey(
                name: "FK_Widget_User_Widgets",
                table: "Widget");

            migrationBuilder.DropIndex(
                name: "IX_Widget_Username",
                table: "Widget");

            migrationBuilder.RenameColumn(
                name: "Widgets",
                table: "Widget",
                newName: "Widget");

            migrationBuilder.RenameIndex(
                name: "IX_Widget_Widgets",
                table: "Widget",
                newName: "IX_Widget_Widget");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Widget",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Widget_User_Widget",
                table: "Widget",
                column: "Widget",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
