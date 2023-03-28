using System.Reflection.Metadata;

namespace TripTrotters.Models
{
    public class Apartament
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public List<Blob> Images { get; set; }

        public int Price { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
