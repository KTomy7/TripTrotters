using TripTrotters.Models;

namespace TripTrotters.ViewModels
{
    public class EditApartmentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int Price { get; set; }

        public int AddressId{ get; set; }

        public Address Address { get; set; }

        public List<Review>? Reviews { get; set; }
        public List<Post>? Posts { get; set; }
        public List<Offer>? Offers { get; set; }

    }
}
