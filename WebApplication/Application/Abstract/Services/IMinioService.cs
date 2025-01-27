namespace Application.Abstract.Services;

public interface IMinioService
{
    Task<bool> UploadFileAsync(string fileName);
    Task<bool> UpdateFileContentAsync(string fileName, string content);
    Task<string?> DownloadFileContentAsync(string fileName);
    Task<bool> DeleteFileAsync(string fileName);
}