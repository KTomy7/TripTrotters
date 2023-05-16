using TripTrotters.Models;

namespace TripTrotters.Services.Abstractions
{
    public interface IUserPostLikeService
    {
        UserPostLike GetByUserAndPostId(int userId, int postId);
        bool PostLikedByUser (int UserId, int PostId);

        bool Add(UserPostLike userPostLikeService);
        bool Delete(UserPostLike userPostLikeService);
        bool Save();
    }
}
