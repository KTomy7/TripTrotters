using Microsoft.EntityFrameworkCore;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;

namespace TripTrotters.Services
{
    public class CommentService : ICommentService
    {
        private readonly TripTrottersDbContext _context;

        public CommentService(TripTrottersDbContext context)
        {
            _context = context;
        }

        public bool Add(Comment comment)
        {
            _context.Comments.Add(comment);
            return Save();
        }

        public bool Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
            return Save();
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _context.Comments.Include(u => u.User).ToListAsync();
        }

        public IEnumerable<Comment> GetAllByPostId(int postId)
        {
            return _context.Comments.Include(u => u.User).Where(c => c.PostId == postId).ToList();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(u => u.User).FirstOrDefaultAsync(c => c.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool Update(Comment comment)
        {
            _context.Comments.Update(comment);
            return Save();
        }
    }
}
