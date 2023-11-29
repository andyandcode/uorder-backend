using Microsoft.AspNetCore.Http;

namespace Application.Files
{
    public interface IFileService
    {
        Task<string> UploadImage(IFormFile image);
    }
}