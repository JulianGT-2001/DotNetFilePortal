using Core.Entities;

namespace Core.Interfaces
{
    public interface IFileUserRepository
    {
        Task AddFileUserAsync(ApplicationUser user, List<Guid> files);
        Task<IEnumerable<FileEntity>> GetAllFilesUserAsync(ApplicationUser user);
        Task<FileEntity?> GetFileByUserIdAsync(ApplicationUser user, Guid id);
        Task RemoveFileByUserIdAsync(ApplicationUser user, FileEntity file);
    }
}