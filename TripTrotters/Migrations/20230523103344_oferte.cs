using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripTrotters.Migrations
{
    /// <inheritdoc />
    public partial class oferte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Apartments_ApartmentId",
                table: "Images");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Offers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Offers");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Apartments_ApartmentId",
                table: "Images",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id");
        }
    }
}
