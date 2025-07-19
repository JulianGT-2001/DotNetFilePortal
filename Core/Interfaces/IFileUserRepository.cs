using Core.Entities;
using Core.Entities.Dto;

namespace Core.Interfaces
{
    public interface IFileUserRepository
    {
        Task AddFileUserAsync(string userId, IEnumerable<Guid> files);
        Task<IEnumerable<FileResponseDto>> GetAllFilesUserAsync(string userId);
        Task<FileResponseDto?> GetFileByUserIdAsync(string userId, Guid id);
        Task RemoveFileByUserIdAsync(string userId, FileResponseDto file);
    }
}