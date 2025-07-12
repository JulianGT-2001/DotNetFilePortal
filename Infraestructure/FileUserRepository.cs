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
        public async Task AddFileUserAsync(ApplicationUser user, List<Guid> files)
        {
            foreach (var fileId in files)
            {
                var fileUser = new FileUser
                {
                    FileId = fileId,
                    UserId = user.Id
                };
                await _db.AddAsync(fileUser);
            }

            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<FileEntity>> GetAllFilesUserAsync(ApplicationUser user)
        {
            return await _db.TbFiles
            .Include(e => e.FileUsers)
            .Where(e => e.FileUsers.All(t => t.UserId == user.Id))
            .ToListAsync();
        }

        public async Task<FileEntity?> GetFileByUserIdAsync(ApplicationUser user, Guid id)
        {
            return await _db.TbFiles
            .Include(e => e.FileUsers)
            .Where(e => e.FileUsers.Any(t => t.UserId == user.Id) && e.Id == id)
            .FirstOrDefaultAsync();
        }

        public async Task RemoveFileByUserIdAsync(ApplicationUser user, FileEntity file)
        {
            var fileUser = await _db.TbFilesUser.Where(t => t.UserId == user.Id && t.FileId == file.Id).FirstOrDefaultAsync();

            _db.Remove(fileUser!);

            await _db.SaveChangesAsync();
        }
        #endregion
    }
}