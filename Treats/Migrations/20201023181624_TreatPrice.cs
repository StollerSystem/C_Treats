using Microsoft.EntityFrameworkCore.Migrations;

namespace Treats.Migrations
{
    public partial class TreatPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TreatPrice",
                table: "Treats",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TreatQuantity",
                table: "OrderTreats",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TreatPrice",
                table: "Treats");

            migrationBuilder.DropColumn(
                name: "TreatQuantity",
                table: "OrderTreats");
        }
    }
}
