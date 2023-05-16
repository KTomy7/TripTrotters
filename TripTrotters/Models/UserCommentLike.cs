using System.ComponentModel.DataAnnotations;

namespace TripTrotters.Models
{
    public class UserCommentLike
    {
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
