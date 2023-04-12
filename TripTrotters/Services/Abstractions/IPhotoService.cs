using CloudinaryDotNet.Actions;

namespace TripTrotters.Services.Abstractions
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
