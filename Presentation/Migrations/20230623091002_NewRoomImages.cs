using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class NewRoomImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageRoom_Rooms_RoomId",
                table: "ImageRoom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageRoom",
                table: "ImageRoom");

            migrationBuilder.RenameTable(
                name: "ImageRoom",
                newName: "ImagesRoom");

            migrationBuilder.RenameIndex(
                name: "IX_ImageRoom_RoomId",
                table: "ImagesRoom",
                newName: "IX_ImagesRoom_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagesRoom",
                table: "ImagesRoom",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesRoom_Rooms_RoomId",
                table: "ImagesRoom",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagesRoom_Rooms_RoomId",
                table: "ImagesRoom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagesRoom",
                table: "ImagesRoom");

            migrationBuilder.RenameTable(
                name: "ImagesRoom",
                newName: "ImageRoom");

            migrationBuilder.RenameIndex(
                name: "IX_ImagesRoom_RoomId",
                table: "ImageRoom",
                newName: "IX_ImageRoom_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageRoom",
                table: "ImageRoom",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageRoom_Rooms_RoomId",
                table: "ImageRoom",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
