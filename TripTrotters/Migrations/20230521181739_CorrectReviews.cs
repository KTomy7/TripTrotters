using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripTrotters.Migrations
{
    /// <inheritdoc />
    public partial class CorrectReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rewies_Apartments_ApartmentId",
                table: "Rewies");

            migrationBuilder.DropForeignKey(
                name: "FK_Rewies_AspNetUsers_UserId",
                table: "Rewies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rewies",
                table: "Rewies");

            migrationBuilder.RenameTable(
                name: "Rewies",
                newName: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_Rewies_UserId",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rewies_ApartmentId",
                table: "Reviews",
                newName: "IX_Reviews_ApartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Apartments_ApartmentId",
                table: "Reviews",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Apartments_ApartmentId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Rewies");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "Rewies",
                newName: "IX_Rewies_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ApartmentId",
                table: "Rewies",
                newName: "IX_Rewies_ApartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rewies",
                table: "Rewies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rewies_Apartments_ApartmentId",
                table: "Rewies",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rewies_AspNetUsers_UserId",
                table: "Rewies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
