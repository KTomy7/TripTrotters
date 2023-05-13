namespace TripTrotters.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int Like { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
