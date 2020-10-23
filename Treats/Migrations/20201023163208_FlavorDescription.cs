using Microsoft.EntityFrameworkCore.Migrations;

namespace Treats.Migrations
{
    public partial class FlavorDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlavorDescription",
                table: "Flavors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlavorDescription",
                table: "Flavors");
        }
    }
}
