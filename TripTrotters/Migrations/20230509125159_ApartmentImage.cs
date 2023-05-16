using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripTrotters.Migrations
{
    /// <inheritdoc />
    public partial class ApartmentImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Apartments_ApartmentId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_ApartmentId",
                table: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Apartments");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ApartmentId",
                table: "Image",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Apartments_ApartmentId",
                table: "Image",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id");
        }
    }
}
