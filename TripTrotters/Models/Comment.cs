using System.ComponentModel.DataAnnotations.Schema;

namespace TripTrotters.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int Like { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public ICollection<UserCommentLike> UsersLikes { get; set; }
    }
}
