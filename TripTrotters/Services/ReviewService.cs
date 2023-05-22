using Microsoft.EntityFrameworkCore;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;

namespace TripTrotters.Services
{
    public class ReviewService : IReviewService
    {
        private readonly TripTrottersDbContext _context;

        public ReviewService(TripTrottersDbContext context)
        {
            _context = context;
        }

        public bool Add(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool Delete(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public async Task<IEnumerable<Review>> GetAll()
        {    
            return await _context.Reviews.Include(r=>r.Apartment).Include(r => r.User).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllByApartmentId(int apartmentId)
        {
            return await _context.Reviews.Include(u => u.User).Where(r => r.Apartment.Id == apartmentId).ToListAsync();
        }

     

        public async Task<Review> GetByIdAsync(int id)
        {
            return await _context.Reviews.Include(u => u.User).FirstOrDefaultAsync(r => r.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

     

        public bool Update(Review review)
        {
            _context.Update(review);
            return Save();          
        }
    }
}
