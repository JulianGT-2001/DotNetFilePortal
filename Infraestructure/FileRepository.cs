using Core.Interfaces;
using Core.Entities;

namespace Infraestructure;

public class FileRepository : IFileRepository
{
    public Task AddFileAsync(List<FileEntity> file)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FileEntity>> GetAllFilesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<FileEntity?> GetFileByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}