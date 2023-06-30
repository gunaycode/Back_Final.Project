using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class RemoveSearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Search_Cities_CityId",
                table: "Search");

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "Search",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 4,
                oldDefaultValue: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_Search_Cities_CityId",
                table: "Search",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Search_Cities_CityId",
                table: "Search");

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "Search",
                type: "int",
                maxLength: 4,
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Search_Cities_CityId",
                table: "Search",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
