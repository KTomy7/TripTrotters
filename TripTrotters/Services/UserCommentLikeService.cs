using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;
using TripTrotters.DataAccess;

namespace TripTrotters.Services
{
    public class UserCommentLikeService : IUserCommentLikeService
    {
        private readonly TripTrottersDbContext _context;

        public UserCommentLikeService(TripTrottersDbContext context) { _context = context; }

        public bool Add(UserCommentLike userCommentLike)
        {
            _context.Add(userCommentLike);
            return Save();
        }

        public bool Delete(UserCommentLike userCommentLike)
        {
            _context.Remove(userCommentLike);
            return Save();
        }

        public UserCommentLike GetByUserAndCommentId(int userId, int commentId)
        {
            return _context.UserCommentLikes.First(upl => upl.UserId == userId && upl.CommentId == commentId);
        }

        public bool CommentLikedByUser(int UserId, int CommentId)
        {
            return _context.UserCommentLikes.Any(upl => upl.UserId == UserId && upl.CommentId == CommentId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
