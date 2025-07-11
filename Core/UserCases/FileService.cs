using Core.Entities;
using Core.Entities.Dto;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

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

    public async Task<FileDownloadDto?> GetFileContentAsync(Guid id)
    {
        var file = await _repository.GetFileByIdAsync(id);

        if (file == null)
            return null;

        if (!File.Exists(file.Path))
            return null;

        var stream = new FileStream(file.Path, FileMode.Open, FileAccess.Read);

        return new FileDownloadDto
        {
            ContentStream = stream,
            ContentType = file.MimeType,
            OriginalName = file.OriginalName
        };
    }

    public async Task DeleteFileAsync(Guid id)
    {
        var file = await _repository.GetFileByIdAsync(id);

        if (file != null)
        {
            if (File.Exists(file.Path))
            {
                File.Delete(file.Path);
            }

            await _repository.RemoveFileAsync(file);
        }
    }
    #endregion
}