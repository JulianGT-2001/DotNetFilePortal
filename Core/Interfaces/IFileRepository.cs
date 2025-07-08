using Core.Entities;

namespace Core.Interfaces;


public interface IFileRepository
{
    Task AddFileAsync(FileEntity file);
    Task<IEnumerable<FileEntity>> GetAllFilesAsync();
    Task<FileEntity?> GetFileByIdAsync(Guid id);
}