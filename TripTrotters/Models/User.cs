using Microsoft.AspNetCore.Identity;

namespace TripTrotters.Models
{
    public class User : IdentityUser<int>
    {
        public string? ImageUrl { get; set; }
        public List<Post>? Posts { get; set; }
        public List<Offer>? Offers { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Comment>? Comments { get; set; }
        public List <Apartment>? Apartments { get; set; }
        public ICollection <UserPostLike> LikedPosts { get; set; }
        public ICollection <UserCommentLike> LikedComments { get; set; }
    }
}
