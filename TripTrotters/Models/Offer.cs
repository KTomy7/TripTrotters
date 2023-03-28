namespace TripTrotters.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AgentId { get; set; }
        public User Agent { get; set; }
        public int ApartamentId { get; set; }
        public Apartament Apartament { get; set; }
    }
}
