using TripTrotters.Models;

namespace TripTrotters.Services.Abstractions
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAll();
        Task<Address> GetByIdAsync(int id);

        bool Add(Address offer);
        bool Update(Address offer);
        bool Delete(Address offer);
        bool Save();
    }
}
