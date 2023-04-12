using TripTrotters.Services.Abstractions;
using TripTrotters.Models;

using Microsoft.EntityFrameworkCore;

using TripTrotters.DataAccess;

namespace TripTrotters.Services
{
	public class PostService : IPostService
	{
		private readonly TripTrottersDbContext _context;

		public PostService(TripTrottersDbContext context)
		{
			_context = context;
		}

		public object Posts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public bool Add(Post p)
		{
			_context.Posts.Add(p);
			return Save();
		}

		public bool Delete(Post p)
		{
			_context.Posts.Remove(p);
			return Save();
		}

		public async Task<IEnumerable<Post>> GetAll()
		{
			return await _context.Posts.Include(a => a.Apartment).ToListAsync();
		}

		public async Task<IEnumerable<Post>> GetAllbyUser(int UserId)
		{
			return await _context.Posts.Include(a => a.Apartment).Include(u => u.User).Where(p => p.UserId == UserId).ToListAsync();
		}

		public async Task<Post> GetByIdAsync(int id)
		{
			return await _context.Posts.Include(a => a.Apartment).Include(u => u.User).FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<IEnumerable<Post>> GetPostsbyUserId(int UserId)
		{
			return await _context.Posts.Include(a => a.Apartment).Include(u => u.User).Where(p => p.UserId == UserId).ToListAsync();
		}

		public bool Save()
		{
			return _context.SaveChanges() >= 0 ? true : false;
		}

		public async Task<IEnumerable<Post>> GetAllbyApartment(int ApartmentId)
		{
			return await _context.Posts.Include(a => a.Apartment).Include(u => u.User).Where(p => p.ApartmentId == ApartmentId).ToListAsync();
		}

		public bool Update(Post p)
		{
			_context.Posts.Update(p);
			return Save();
		}
	}
}
