using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripTrotters.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Location { get; set; }

        [ForeignKey("Apartment")]
        public int? ApartmentId { get; set; }
       
        [ForeignKey("Post")]
        public int? PostId { get; set; }
    }
}
