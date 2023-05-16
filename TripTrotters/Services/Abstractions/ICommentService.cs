using TripTrotters.Models;


namespace TripTrotters.Services.Abstractions
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAll();
        IEnumerable<Comment> GetAllByPostId(int postId);
        Task<Comment> GetByIdAsync(int id);

        bool Add(Comment comment);
        bool Update(Comment comment);
        bool Delete(Comment comment);
        bool Save();

    }
}
