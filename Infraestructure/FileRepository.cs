using Core.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure;

public class FileRepository : IFileRepository
{
    #region Atributos
    private readonly MyDbContext _db;
    #endregion

    #region Constructor
    public FileRepository(MyDbContext db)
    {
        _db = db;
    }
    #endregion
    public async Task<IEnumerable<Guid>> AddFileAsync(List<FileEntity> file)
    {
        List<Guid> fileGuids = new List<Guid>();
        foreach (var item in file)
        {
            await _db.AddAsync(item);
            fileGuids.Add(item.Id);
        }

        await _db.SaveChangesAsync();

        return fileGuids;
    }

    public async Task<IEnumerable<FileEntity>> GetAllFilesAsync()
    {

        return await _db.TbFiles.ToListAsync();
    }

    public async Task<FileEntity?> GetFileByIdAsync(Guid id)
    {
        return await _db.TbFiles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task RemoveFileAsync(FileEntity file)
    {
        _db.Remove(file);

        await _db.SaveChangesAsync();
    }
}