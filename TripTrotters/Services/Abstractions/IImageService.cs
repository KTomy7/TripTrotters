using TripTrotters.Models;

namespace TripTrotters.Services.Abstractions
{
    public interface IImageService
    {
        bool Add(Image i);
        bool Save();
    }
}
