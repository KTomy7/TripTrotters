using Microsoft.AspNetCore.Components.Routing;
using System.Reflection.Metadata;

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
        public List<Comment> Comments { get; set; }
        public int ApartamentId { get; set; }
        public Apartament Apartament { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
