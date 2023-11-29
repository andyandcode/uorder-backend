using Application.Files;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("file")]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Post to get image data.
        /// </summary>
        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            var result = await _fileService.UploadImage(image);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}