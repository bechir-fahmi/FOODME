using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Platform.Shared.CloudinaryHelpers
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<VideoUploadResult> AddVideoAsync(IFormFile file);
        Task<DeletionResult> DeleteFileAsync(string publicId);
    }
}
