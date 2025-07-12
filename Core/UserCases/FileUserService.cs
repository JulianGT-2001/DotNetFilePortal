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
        public async Task RegisterFileUserAsync(ApplicationUser user, List<Guid> guids)
        {
            await _repository.AddFileUserAsync(user, guids);
        }

        public async Task<IEnumerable<FileEntity>> GetFilesUserAsync(ApplicationUser user)
        {
            return await _repository.GetAllFilesUserAsync(user);
        }

        public async Task<FileEntity?> GetFileUserAsync(ApplicationUser user, Guid fileId)
        {
            return await _repository.GetFileByUserIdAsync(user, fileId);
        }

        public async Task<FileDownloadDto?> GetFileContentUserAsync(ApplicationUser user, Guid fileId)
        {
            var file = await _repository.GetFileByUserIdAsync(user, fileId);

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

        public async Task DeleteUserFileAsync(ApplicationUser user, Guid id)
        {
            var file = await _repository.GetFileByUserIdAsync(user, id);

            if (file != null)
            {
                if (File.Exists(file.Path))
                {
                    File.Delete(file.Path);
                }

                await _repository.RemoveFileByUserIdAsync(user, file);
            }
        }
        #endregion
    }
}