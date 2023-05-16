using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripTrotters.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        [ForeignKey("Apartment")]
        public int? ApartmentId { get; set; }
        public Apartment? Apartment { get; set; }
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public Post? Post { get; set; }
    }
}
