using Microsoft.EntityFrameworkCore.Migrations;

namespace reusable_modules_sharing_server.Migrations
{
    public partial class Changed_property_names_to_match_google_results : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "User",
                newName: "Familyname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Familyname",
                table: "User",
                newName: "Surname");
        }
    }
}
