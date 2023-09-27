using Identity_Provider.Service.Interfaces.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Provider.WebApi.Controllers.Files
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IFileService _fileService;
        public ImageController(IFileService _fileService)
        {
            this._fileService = _fileService;
        }

        [HttpGet("getall")] 
        [AllowAnonymous]
        public async Task<IActionResult> FileGetAllAsync()
            => Ok(await _fileService.GetAllAsync());

        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> FileCreateAsync(IFormFile file)
            => Ok(await _fileService.UploadImageAsync(file));

        [HttpDelete]
        [Authorize("Admin")]
        public async Task<IActionResult> FileDeleteAsync(string filePath)
            => Ok(await _fileService.DeleteFileAsync(filePath));


        [HttpGet("imagename")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFile(string fileName)
        {
            try
            {
                var fileStream = await _fileService.GetFileStreamAsync(fileName);

                if (fileStream == null)
                {
                    return NotFound(); 
                }

                return File(fileStream, "application/octet-stream");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the file.");
            }
        }
    }
}
