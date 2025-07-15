using Core.Entities;
using Core.Entities.Dto;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Core.UserCases
{
    public class FileUserService : IFileUserService
    {
        #region Atributos
        private readonly IFileUserRepository _repository;
        #endregion
        #region Constructor
        public FileUserService(IFileUserRepository repository)
        {
            _repository = repository;
        }
        #endregion
        #region Metodos
        public async Task RegisterFileUserAsync(string userId, IEnumerable<Guid> guids)
        {
            await _repository.AddFileUserAsync(userId, guids);
        }

        public async Task<IEnumerable<FileEntity>> GetFilesUserAsync(string userId)
        {
            return await _repository.GetAllFilesUserAsync(userId);
        }

        public async Task<FileEntity?> GetFileUserAsync(string userId, Guid fileId)
        {
            return await _repository.GetFileByUserIdAsync(userId, fileId);
        }

        public async Task<FileDownloadDto?> GetFileContentUserAsync(string userId, Guid fileId)
        {
            var file = await _repository.GetFileByUserIdAsync(userId, fileId);

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

        public async Task DeleteUserFileAsync(string userId, Guid id)
        {
            var file = await _repository.GetFileByUserIdAsync(userId, id);

            if (file != null)
            {
                await _repository.RemoveFileByUserIdAsync(userId, file);
            }
        }
        #endregion
    }
}