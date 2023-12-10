using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace Application.Files
{
    public class FileService : IFileService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public FileService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _containerName = "dish";
        }

        public async Task<string> UploadImage(IFormFile image)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                var blobName = Guid.NewGuid().ToString(); // Unique key for the image

                using (var stream = image.OpenReadStream())
                {
                    await containerClient.UploadBlobAsync(blobName, stream);
                }

                var blobClient = containerClient.GetBlobClient(blobName);
                var sasToken = "sp=r&st=2023-11-29T14:14:41Z&se=2024-01-31T22:14:41Z&sv=2022-11-02&sr=c&sig=DdHwadgWZRLdEN7OlFw2%2B8qRMemEOoYsbRdEuND352c%3D";
                // Trả về đường dẫn URL đầy đủ đến ảnh
                var imageUrl = $"{blobClient.Uri}?{sasToken}";
                return imageUrl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}