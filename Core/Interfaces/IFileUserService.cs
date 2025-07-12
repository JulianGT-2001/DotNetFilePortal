using Core.Entities;
using Core.Entities.Dto;

namespace Core.Interfaces
{
    public interface IFileUserService
    {
        Task RegisterFileUserAsync(string userId, IEnumerable<Guid> guids);
        Task<IEnumerable<FileEntity>> GetFilesUserAsync(string userId);
        Task<FileEntity?> GetFileUserAsync(string userId, Guid fileId);
        Task<FileDownloadDto?> GetFileContentUserAsync(string userId, Guid fileId);
        Task DeleteUserFileAsync(string userId, Guid id);
    }
}