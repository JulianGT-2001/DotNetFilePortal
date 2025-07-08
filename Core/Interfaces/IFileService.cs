using Core.Entities;

namespace Core.Interfaces;


public interface IFileService
{
    Task RegisterFileAsync(FileEntity file);
    Task<IEnumerable<FileEntity>> GetFilesAsync();
    Task<FileEntity?> GetFileAsync(Guid id);
}