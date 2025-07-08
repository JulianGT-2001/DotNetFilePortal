namespace Core.Interfaces;

using Core.Entities;

public interface IFileService
{
    Task RegisterFileAsync(File file);
    Task<IEnumerable<File>> GetFilesAsync();
    Task<File?> GetFileAsync();
}