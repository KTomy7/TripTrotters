using System.ComponentModel.DataAnnotations;

namespace TripTrotters.Models
{
    public class UserPostLike
    {
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
