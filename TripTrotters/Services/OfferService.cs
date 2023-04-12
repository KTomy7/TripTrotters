using Microsoft.EntityFrameworkCore;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;

namespace TripTrotters.Services
{
    public class OfferService : IOfferService
    {
        private readonly TripTrottersDbContext _context;
        public OfferService(TripTrottersDbContext context)
        {
            _context = context;
        }

        public bool Add(Offer offer)
        {
            _context.Add(offer);
            return Save();
        }

        public bool Delete(Offer offer)
        {
            _context.Remove(offer);
            return Save();
        }

        public async Task<IEnumerable<Offer>> GetAll()
        {
            return await _context.Offers.Include(a => a.Apartment.Address).ToListAsync();
        }

        public async Task<Offer> GetByIdAsync(int id)
        {
            return await _context.Offers.Include(a => a.Apartment.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Offer>> GetOfferByCity(string city)
        {
            return await _context.Offers.Where(o => o.Apartment.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<IEnumerable<Offer>> GetOfferByCountry(string country)
        {
            return await _context.Offers.Where(o => o.Apartment.Address.Country.Contains(country)).ToListAsync();
        }

        public async Task<IEnumerable<Offer>> GetOfferByPrice(double startPrice, double endPrice)
        {
            return await _context.Offers.Where(o => o.Apartment.Price >= startPrice && o.Apartment.Price <= endPrice).ToListAsync();
        }

        public async Task<IEnumerable<Offer>> GetOfferByDate(DateTime startDate, DateTime endDate)
        {
            return await _context.Offers.Where(o => o.StartDate >= startDate && o.EndDate <= endDate).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Offer offer)
        {
            _context.Update(offer);
            return Save();
        }
    }
}
