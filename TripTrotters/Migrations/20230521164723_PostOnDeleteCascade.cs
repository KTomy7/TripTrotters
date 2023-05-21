using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripTrotters.Migrations
{
    /// <inheritdoc />
    public partial class PostOnDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCommentLikes_Comments_CommentId",
                table: "UserCommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPostLikes_Posts_PostId",
                table: "UserPostLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommentLikes_Comments_CommentId",
                table: "UserCommentLikes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPostLikes_Posts_PostId",
                table: "UserPostLikes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCommentLikes_Comments_CommentId",
                table: "UserCommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPostLikes_Posts_PostId",
                table: "UserPostLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommentLikes_Comments_CommentId",
                table: "UserCommentLikes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPostLikes_Posts_PostId",
                table: "UserPostLikes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
