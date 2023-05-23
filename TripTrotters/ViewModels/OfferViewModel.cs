using System.ComponentModel.DataAnnotations.Schema;
using TripTrotters.Models;

namespace TripTrotters.ViewModels
{
    public class OfferViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
        public int AgentId { get; set; }
        public int ApartmentId { get; set; }

        public double Price { get; set; }
     }
}
