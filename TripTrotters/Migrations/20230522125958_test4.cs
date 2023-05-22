using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripTrotters.Migrations
{
    /// <inheritdoc />
    public partial class test4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Apartments_ApartmentId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Apartments_ApartmentId",
                table: "Images",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Apartments_ApartmentId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Apartments_ApartmentId",
                table: "Images",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id");
        }
    }
}
