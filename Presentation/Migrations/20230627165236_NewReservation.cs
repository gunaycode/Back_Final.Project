using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class NewReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomCategoryId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomCategoryId",
                table: "Reservations",
                column: "RoomCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_RoomCategories_RoomCategoryId",
                table: "Reservations",
                column: "RoomCategoryId",
                principalTable: "RoomCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_RoomCategories_RoomCategoryId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_RoomCategoryId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RoomCategoryId",
                table: "Reservations");
        }
    }
}
