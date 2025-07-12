using Core.Entities;
using Core.Entities.Dto;

namespace Core.Interfaces
{
    public interface IFileUserService
    {
        Task RegisterFileUserAsync(ApplicationUser user, List<Guid> guids);
        Task<IEnumerable<FileEntity>> GetFilesUserAsync(ApplicationUser user);
        Task<FileEntity?> GetFileUserAsync(ApplicationUser user, Guid fileId);
        Task<FileDownloadDto?> GetFileContentUserAsync(ApplicationUser user, Guid fileId);
        Task DeleteUserFileAsync(ApplicationUser user, Guid id);
    }
}