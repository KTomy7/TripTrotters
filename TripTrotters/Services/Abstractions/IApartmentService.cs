using TripTrotters.Models;

namespace TripTrotters.Services.Abstractions
{
    public interface IApartmentService
    {
        Task<IEnumerable<Apartment>> GetAll();
        Task<Apartment> GetByIdAsync(int id);
        Task<IEnumerable<Apartment>> GetApartmentbyAddress(string city);
        Task<IEnumerable<Apartment>> GetByUserIdAsync(int userId);
        bool Add(Apartment ap);
        bool Update(Apartment ap);
        bool Delete(Apartment ap);
        bool Save();
    }
}
