using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Core.UserCases;

public class FileService : IFileService
{
    #region attributes
    private readonly IFileRepository _repository;
    private readonly IConfiguration _configuration;
    #endregion
    #region constructor
    public FileService(IFileRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }
    #endregion
    #region methods
    public async Task RegisterFileAsync(IFormFileCollection files, string userId)
    {
        List<FileEntity> filesEntity = new List<FileEntity>();
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file.FileName);
            var fileSize = file.Length;
            var mimeType = file.ContentType;

            var uniqueName = $"{Guid.NewGuid()}_{fileName}";
            var principalRoute = _configuration["DownloadPath"]!;
            var path = Path.Combine(principalRoute, uniqueName);

            if (!Directory.Exists(principalRoute))
            {
                Directory.CreateDirectory(principalRoute);
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var entity = new FileEntity
            {
                OriginalName = fileName,
                Path = path,
                SizeInBytes = fileSize,
                MimeType = mimeType,
                UploadedBy = userId,
                UploadedAt = DateTime.Now
            };

            filesEntity.Add(entity);
        }

        await _repository.AddFileAsync(filesEntity);
    }

    public Task<IEnumerable<FileEntity>> GetFilesAsync()
    {
        return _repository.GetAllFilesAsync();
    }

    public async Task<FileEntity?> GetFileAsync(Guid id)
    {
        return await _repository.GetFileByIdAsync(id);
    }
    #endregion
}