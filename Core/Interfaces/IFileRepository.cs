using Core.Entities;

namespace Core.Interfaces;


public interface IFileRepository
{
    Task<IEnumerable<Guid>> AddFileAsync(List<FileEntity> file);
    Task<IEnumerable<FileEntity>> GetAllFilesAsync();
    Task<FileEntity?> GetFileByIdAsync(Guid id);
    Task RemoveFileAsync(FileEntity file);
}