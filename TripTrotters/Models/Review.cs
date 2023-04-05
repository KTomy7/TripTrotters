using System.ComponentModel.DataAnnotations.Schema;

namespace TripTrotters.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Apartament")]
        public int ApartamentId { get; set; }
        public Apartament Apartament { get; set; }

    }
}
