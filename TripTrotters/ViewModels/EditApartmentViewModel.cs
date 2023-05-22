using TripTrotters.Models;

namespace TripTrotters.ViewModels
{
    public class EditApartmentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int AddressId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public IEnumerable<IFormFile>? NewImages { get; set; }
        public List<string>? ImageUrls { get; set; }
        public List<string>? ImagesToDelete { get; set; }
    }
}
