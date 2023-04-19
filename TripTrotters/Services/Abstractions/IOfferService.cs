using TripTrotters.Models;

namespace TripTrotters.Services.Abstractions
{
    public interface IOfferService
    {
        Task<IEnumerable<Offer>> GetAll();
        Task<Offer> GetByIdAsync(int id);
        Task<IEnumerable<Offer>> GetOfferByCountry(string country);
        Task<IEnumerable<Offer>> GetOfferByCity(string city);
        Task<IEnumerable<Offer>> GetOfferByPrice(double startPrice, double endPrice);
        Task<IEnumerable<Offer>> GetOfferByDate(DateTime startDate, DateTime endDate);

        bool Add(Offer offer);
        bool Update(Offer offer);
        bool Delete(Offer offer);
        bool Save();
       
    }
}
