using Microsoft.EntityFrameworkCore;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;

namespace TripTrotters.Services
{
    public class AddressService : IAddressService
    {
        private readonly TripTrottersDbContext _context;
        public AddressService(TripTrottersDbContext context)
        {
            _context = context;
        }

        public bool Add(Address address)
        {
            _context.Add(address);
            return Save();
        }

        public bool Delete(Address address)
        {
            _context.Remove(address);
            return Save();
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Address address)
        {
            _context.Update(address);
            return Save();
        }
    }
}
