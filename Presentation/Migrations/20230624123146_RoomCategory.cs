using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class RoomCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryNameId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RoomCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActived = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CategoryNameId",
                table: "Rooms",
                column: "CategoryNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomCategories_CategoryNameId",
                table: "Rooms",
                column: "CategoryNameId",
                principalTable: "RoomCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomCategories_CategoryNameId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomCategories");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_CategoryNameId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CategoryNameId",
                table: "Rooms");
        }
    }
}
