using Core.Entities;
using Core.Interfaces;

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
    public async Task RegisterFileAsync(FileEntity file)
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