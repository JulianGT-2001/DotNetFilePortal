using Core.Entities;

namespace Core.Interfaces;


public interface IFileService
{
    Task RegisterFileAsync(File file);
    Task<IEnumerable<File>> GetFilesAsync();
    Task<File?> GetFileAsync();
}