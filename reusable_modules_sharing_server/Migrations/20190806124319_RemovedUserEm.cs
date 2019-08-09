using Microsoft.EntityFrameworkCore.Migrations;

namespace WidgetServer.Migrations
{
    public partial class RemovedUserEm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widget_User_Widgets",
                table: "Widget");

            migrationBuilder.DropIndex(
                name: "IX_Widget_Username",
                table: "Widget");

            migrationBuilder.DropIndex(
                name: "IX_Widget_Widgets",
                table: "Widget");

            migrationBuilder.AlterColumn<string>(
                name: "Widgets",
                table: "Widget",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Widgets",
                table: "Widget",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Widget_Username",
                table: "Widget",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_Widget_Widgets",
                table: "Widget",
                column: "Widgets");

            migrationBuilder.AddForeignKey(
                name: "FK_Widget_User_Widgets",
                table: "Widget",
                column: "Widgets",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
