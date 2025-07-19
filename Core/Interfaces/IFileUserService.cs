using Core.Entities;
using Core.Entities.Dto;

namespace Core.Interfaces
{
    public interface IFileUserService
    {
        Task RegisterFileUserAsync(string userId, IEnumerable<Guid> guids);
        Task<IEnumerable<FileResponseDto>> GetFilesUserAsync(string userId);
        Task<FileResponseDto?> GetFileUserAsync(string userId, Guid fileId);
        Task<FileDownloadDto?> GetFileContentUserAsync(string userId, Guid fileId);
        Task DeleteUserFileAsync(string userId, Guid id);
    }
}