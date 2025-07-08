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
    public async Task RegisterFileAsync(File file)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<File>> GetFilesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<File> GetFileAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    #endregion
}