using TripTrotters.Models;

namespace TripTrotters.Services.Abstractions
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAll();
        Task<IEnumerable<Review>> GetAllByApartmentId(int ApartmentId);
        Task<Review> GetByIdAsync(int id);

        bool Add(Review review);
        bool Update(Review review);
        bool Delete(Review review);
        bool Save();
    }
}
