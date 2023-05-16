using CloudinaryDotNet.Actions;
using TripTrotters.Models;

namespace TripTrotters.Services.Abstractions
{
    public interface ICloudinaryImageService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
