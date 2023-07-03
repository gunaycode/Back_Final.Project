using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class CommnetHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_HotelId",
                table: "Comments",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Hotels_HotelId",
                table: "Comments",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Hotels_HotelId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_HotelId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Comments");
        }
    }
}
