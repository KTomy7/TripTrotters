using TripTrotters.Models;

namespace TripTrotters.ViewModels
{
    public class CreatePostViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Budget { get; set; }
        public int Likes { get; set; }
        public DateTime Date { get; set; }
        public List<Comment>? Comments { get; set; }
        public int ApartmentId { get; set; }
        public int UserId { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
