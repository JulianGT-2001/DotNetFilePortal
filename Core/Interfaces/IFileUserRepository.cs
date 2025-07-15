using Core.Entities;

namespace Core.Interfaces
{
    public interface IFileUserRepository
    {
        Task AddFileUserAsync(string userId, IEnumerable<Guid> files);
        Task<IEnumerable<FileEntity>> GetAllFilesUserAsync(string userId);
        Task<FileEntity?> GetFileByUserIdAsync(string userId, Guid id);
        Task RemoveFileByUserIdAsync(string userId, FileEntity file);
    }
}