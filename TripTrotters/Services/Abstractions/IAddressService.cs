using TripTrotters.Models;

namespace TripTrotters.Services.Abstractions
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAll();
        Task<Address> GetByIdAsync(int id);

        bool Add(Address address);
        bool Update(Address address);
        bool Delete(Address address);
        bool Save();
    }
}
