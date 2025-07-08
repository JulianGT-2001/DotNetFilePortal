namespace Core.Interfaces;


public interface IFileRepository
{
    Task AddFileAsync(File file);
    Task<IEnumerable<File>> GetAllFilesAsync();
    Task<File?> GetFileByIdAsync(Guid id);
}