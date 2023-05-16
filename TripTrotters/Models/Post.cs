using System.ComponentModel.DataAnnotations.Schema;

namespace TripTrotters.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Budget { get; set; }
        public int Likes { get; set; }
        public DateTime Date { get; set; }
        public List<Comment>? Comments { get; set; }
        [ForeignKey("Apartment")]
        public int ApartmentId { get; set; }
        public Apartment? Apartment { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<Image>? Images { get; set; }
		public ICollection<UserPostLike> UsersLikes { get; set; }
    }
}
