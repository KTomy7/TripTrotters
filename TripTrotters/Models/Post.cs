using System.ComponentModel.DataAnnotations.Schema;

namespace TripTrotters.Models
{
    public class Post
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Budget { get; set; }
        public int Likes { get; set; }
        public DateTime Date { get; set; }
        public List<Comment>? Comments { get; set; }
        [ForeignKey("Apartament")]
        public int ApartamentId { get; set; }
        public Apartament Apartament { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
