using System.ComponentModel.DataAnnotations.Schema;
using TripTrotters.Models;

namespace TripTrotters.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        
        public int UserId { get; set; }
        public User? User { get; set; }

        public int ApartmentId { get; set; }
        public Apartment? Apartment { get; set; }
    }
}
