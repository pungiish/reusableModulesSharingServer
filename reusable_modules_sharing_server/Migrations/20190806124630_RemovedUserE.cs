using Microsoft.EntityFrameworkCore.Migrations;

namespace WidgetServer.Migrations
{
    public partial class RemovedUserE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Widgets",
                table: "Widget",
                newName: "Widget");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Widget",
                table: "Widget",
                newName: "Widgets");
        }
    }
}
