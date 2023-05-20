using System.ComponentModel.DataAnnotations.Schema;
using TripTrotters.Models;

namespace TripTrotters.ViewModels
{
    public class CreateApartmentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int OwnerId { get; set; }
        public int AddressId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
}

