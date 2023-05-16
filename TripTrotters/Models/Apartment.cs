using System.ComponentModel.DataAnnotations.Schema;

namespace TripTrotters.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public List <Review>? Reviews { get; set; }
        public List<Post>? Posts { get; set; }
        public List<Offer>? Offers { get; set; }
        public List<Image>? Images { get; set; }
    }
}
