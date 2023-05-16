using TripTrotters.Models;

namespace TripTrotters.Services.Abstractions
{
    public interface IUserCommentLikeService
    {
        UserCommentLike GetByUserAndCommentId(int userId, int commentId);
        bool CommentLikedByUser(int UserId, int commentId);

        bool Add(UserCommentLike userCommentLike);
        bool Delete(UserCommentLike userCommentLike);
        bool Save();
    }
}
