using Core.Entities;

namespace Core.Interfaces;


public interface IFileRepository
{
    Task AddFileAsync(List<FileEntity> file);
    Task<IEnumerable<FileEntity>> GetAllFilesAsync();
    Task<FileEntity?> GetFileByIdAsync(Guid id);
    Task RemoveFileAsync(FileEntity file);
}