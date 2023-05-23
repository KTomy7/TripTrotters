using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;

namespace TripTrotters.Services
{

    public class ApartmentService : IApartmentService
    {
        private readonly TripTrottersDbContext _context;


        public ApartmentService(TripTrottersDbContext context)
        {
            _context = context;

        }
        public bool Add(Apartment ap)
        {
            _context.Add(ap);
            return Save();
        }

        public bool Delete(Apartment ap)
        {
            _context.Remove(ap);
            return Save();

        }

        public async Task<IEnumerable<Apartment>> GetAll()
        {
            return await _context.Apartments.Include(a => a.Address).ToListAsync();
        }


        public async Task<IEnumerable<Apartment>> GetApartmentbyAddress(string city)
        {
            return await _context.Apartments.Where(a => a.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Apartment> GetByIdAsync(int id)
        {
            return await _context.Apartments.Include(a => a.Address).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Apartment>> GetByUserIdAsync(int userId)
        {
            return await _context.Apartments.Include(a => a.Address).Where(a => a.OwnerId == userId).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Apartment ap)
        { 
            _context.Update(ap);
            return Save();
        }
    }
}
