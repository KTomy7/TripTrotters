using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;

namespace TripTrotters.Services
{
    public class UserPostLikeService : IUserPostLikeService
    {
        private readonly TripTrottersDbContext _context;

        public UserPostLikeService(TripTrottersDbContext context) { _context = context; }

        public bool Add(UserPostLike userPostLike)
        {
            _context.Add(userPostLike);
            return Save();
        }

        public bool Delete(UserPostLike userPostLike)
        {
            _context.Remove(userPostLike);
            return Save();
        }

        public UserPostLike GetByUserAndPostId(int userId, int postId)
        {
            return _context.UserPostLikes.First(upl => upl.UserId == userId && upl.PostId == postId);
        }

        public bool PostLikedByUser(int UserId, int PostId)
        {
            return _context.UserPostLikes.Any(upl => upl.UserId == UserId && upl.PostId == PostId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
