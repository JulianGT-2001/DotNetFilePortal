using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Core.UserCases;

public class FileService : IFileService
{
    #region attributes
    private readonly IFileRepository _repository;
    #endregion
    #region constructor
    public FileService(IFileRepository repository)
    {
        _repository = repository;
    }
    #endregion
    #region methods
    public async Task RegisterFileAsync(IFormFileCollection files, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FileEntity>> GetFilesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<FileEntity> GetFileAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    #endregion
}