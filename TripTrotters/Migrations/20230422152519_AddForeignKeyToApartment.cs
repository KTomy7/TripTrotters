using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripTrotters.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToApartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_OwnerId",
                table: "Apartments",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_AspNetUsers_OwnerId",
                table: "Apartments",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_AspNetUsers_OwnerId",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_OwnerId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Apartments");
        }
    }
}
