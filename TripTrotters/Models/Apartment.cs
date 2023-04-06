namespace TripTrotters.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Price { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Post>? Posts { get; set; }
        public List<Offer>? Offers { get; set; }
    }
}
