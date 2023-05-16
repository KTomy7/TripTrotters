using TripTrotters.DataAccess;
using TripTrotters.Models;

namespace TripTrotters.Services.Abstractions
{
    public class ImageService : IImageService
    {
        private readonly TripTrottersDbContext _context;

        public ImageService(TripTrottersDbContext context)
        {
            _context = context;
        }

        public bool Add(Image i)
        {
            _context.Images.Add(i);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
