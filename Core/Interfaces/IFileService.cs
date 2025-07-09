using Core.Entities;
using Core.Entities.Dto;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces;


public interface IFileService
{
    Task RegisterFileAsync(IFormFileCollection files, string userId);
    Task<IEnumerable<FileEntity>> GetFilesAsync();
    Task<FileEntity?> GetFileAsync(Guid id);
    Task<FileDownloadDto?> GetFileContentAsync(Guid id);
}