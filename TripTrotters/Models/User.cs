namespace TripTrotters.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public UserType Type { get; set; }

        public List<Post> Posts { get; set; }

        public List<Offer> Offers { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Comment> Comments { get; set; }

    }
}
