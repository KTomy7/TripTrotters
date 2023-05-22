using TripTrotters.Models;

namespace TripTrotters.Services.Abstractions
{
    public interface IImageService
    {
        Task<IEnumerable<Image>> GetAllImages();
        Task<IEnumerable<Image>> GetAllImagesByApartmentId(int id);
        Task<IEnumerable<Image>> GetAllImagesByPostId(int id);
        Task<Image> GetImageByUrl(string url);
        bool Add(Image i);
        bool Delete(Image i);
        bool Save();
    }
}
