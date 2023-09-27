using Identity_Provider.DataAccess.ViewModels.Files;
using Microsoft.AspNetCore.Http;

namespace Identity_Provider.Service.Interfaces.Files;

public interface IFileService
{
    public Task<List<FileViewModel>> GetAllAsync();
    public Task<bool> DeleteFileAsync(string subpath);
    public Task<string> UploadImageAsync(IFormFile file);
    public Task<Stream> GetFileStreamAsync(string fileName);
}
