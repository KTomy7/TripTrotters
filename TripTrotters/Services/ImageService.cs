using Microsoft.EntityFrameworkCore;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;

namespace TripTrotters.Services
{
    public class ImageService : IImageService
    {
        private readonly TripTrottersDbContext _context;

        public ImageService(TripTrottersDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Image>> GetAllImages()
        {
            return await _context.Images.ToListAsync();
        }

        public async Task<IEnumerable<Image>> GetAllImagesByApartmentId(int id)
        {
            return await _context.Images.Where(i => i.ApartmentId == id).ToListAsync();
        }

        public async Task<IEnumerable<Image>> GetAllImagesByPostId(int id)
        {
            return await _context.Images.Where(i => i.PostId == id).ToListAsync();
        }

        public async Task<Image> GetImageByUrl(string url)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.ImageUrl == url);
        }

        public bool Add(Image i)
        {
            _context.Images.Add(i);
            return Save();
        }

        public bool Delete(Image i)
        {
            _context.Images.Remove(i);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
