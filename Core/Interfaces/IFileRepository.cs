namespace Core.Interfaces;

using Core.Entities;

public interface IFileRepository
{
    Task AddFileAsync(File file);
    Task<IEnumerable<File>> GetAllFilesAsync();
    Task<File?> GetFileByIdAsync(Guid id);
}