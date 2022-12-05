using CloudinaryDotNet.Actions;

namespace RunGroubweb.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicid);
    }
}
