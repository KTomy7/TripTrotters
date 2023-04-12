using TripTrotters.Models;

namespace TripTrotters.Services.Abstractions
{
	public interface IPostService
	{
		object Posts { get; set; }

		Task<IEnumerable<Post>> GetAll();
		Task<Post> GetByIdAsync(int id);
		Task<IEnumerable<Post>> GetAllbyID(int UserId);

		bool Add(Post p);
		bool Update(Post p);
		bool Delete(Post p);
		bool Save();
	}
}
