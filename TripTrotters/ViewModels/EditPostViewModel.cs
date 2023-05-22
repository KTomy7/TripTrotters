using TripTrotters.Models;

namespace TripTrotters.ViewModels
{
    public class EditPostViewModel
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
        public IEnumerable<IFormFile>? NewImages { get; set; }
        public List<string>? ImageUrls { get; set; }
        public List<string>? ImagesToDelete { get; set; }
    }
}
