using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RunGroubweb.Helpers;
using RunGroubweb.Interfaces;
using System.Net.WebSockets;

namespace RunGroubweb.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloundinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloundinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length>0)
            {
                using var stream = file.OpenReadStream();
                var uploadparams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")

                };
                uploadResult = await _cloundinary.UploadAsync(uploadparams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicid)
        {
            var deleteparams = new DeletionParams(publicid);
            var result = await _cloundinary.DestroyAsync(deleteparams);

            return result;
        }
    }
}
