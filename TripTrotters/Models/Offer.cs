using System.ComponentModel.DataAnnotations.Schema;

namespace TripTrotters.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("Agent")]
        public int AgentId { get; set; }
        public User Agent { get; set; }
        [ForeignKey("Apartament")]
        public int ApartamentId { get; set; }
        public Apartament Apartament { get; set; }
    }
}
