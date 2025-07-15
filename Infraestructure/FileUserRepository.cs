using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure
{
    public class FileUserRepository : IFileUserRepository
    {
        #region Atributos
        private readonly MyDbContext _db;
        #endregion

        #region Constructor
        public FileUserRepository(MyDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Metodos
        public async Task AddFileUserAsync(string userId, IEnumerable<Guid> files)
        {
            foreach (var fileId in files)
            {
                var fileUser = new FileUser
                {
                    FileId = fileId,
                    UserId = userId
                };
                await _db.AddAsync(fileUser);
            }

            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<FileEntity>> GetAllFilesUserAsync(string userId)
        {
            return await _db.TbFiles
            .Include(e => e.FileUsers)
            .Where(e => e.FileUsers.All(t => t.UserId == userId))
            .ToListAsync();
        }

        public async Task<FileEntity?> GetFileByUserIdAsync(string userId, Guid id)
        {
            return await _db.TbFiles
            .Include(e => e.FileUsers)
            .Where(e => e.FileUsers.Any(t => t.UserId == userId) && e.Id == id)
            .FirstOrDefaultAsync();
        }

        public async Task RemoveFileByUserIdAsync(string userId, FileEntity file)
        {
            var fileUser = await _db.TbFilesUser.Where(t => t.UserId == userId && t.FileId == file.Id).FirstOrDefaultAsync();

            _db.Remove(fileUser!);

            await _db.SaveChangesAsync();
        }
        #endregion
    }
}