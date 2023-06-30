using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class AddHotelProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Breakfast",
                table: "Hotels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Location",
                table: "Hotels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Parking",
                table: "Hotels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Pet",
                table: "Hotels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Pool",
                table: "Hotels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WiFi",
                table: "Hotels",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breakfast",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Parking",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Pet",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Pool",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "WiFi",
                table: "Hotels");
        }
    }
}
