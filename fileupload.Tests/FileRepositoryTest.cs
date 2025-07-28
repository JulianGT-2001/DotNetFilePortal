using Core.Entities;
using Infraestructure;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.Testing.Moq;
using Moq;
using System.Threading.Tasks;

namespace fileupload.Tests
{
    public class FileRepositoryTest
    {
        [Fact]
        public async Task AddFileAsync_DoesNotThrow_WhenListIsNull()
        {
            var mockDbSet = new Mock<DbSet<FileEntity>>();
            var mockDbContext = new Mock<MyDbContext>();

            mockDbContext.Setup(m => m.Set<FileEntity>()).Returns(mockDbSet.Object);
            mockDbContext.Setup(m => m.AddAsync(It.IsAny<FileEntity>(), default))
                .ReturnsAsync((FileEntity entity, CancellationToken _) => { return null; });

            FileRepository fileRepository = new FileRepository(mockDbContext.Object);
            List<FileEntity> files = new List<FileEntity>();

            var result = await fileRepository.AddFileAsync(files);

            mockDbContext.Verify(m => m.AddAsync(It.IsAny<FileEntity>(), default), Times.Never);
            mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.Empty(result);
        }

        [Fact]
        public async Task AddFileAsync_ReturnsGuids_WhenListHasMultipleItems()
        {
            var files = new List<FileEntity>
            {
                new FileEntity { Id = Guid.NewGuid() },
                new FileEntity { Id = Guid.NewGuid() }
            };

            var mockDbContext = new Mock<MyDbContext>();
            mockDbContext.Setup(x => x.AddAsync(It.IsAny<FileEntity>(), default))
                        .ReturnsAsync((FileEntity f, CancellationToken _) => null);
            mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1);

            var repo = new FileRepository(mockDbContext.Object);
            var result = await repo.AddFileAsync(files);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddFileAsync_Throw_WhenArgumentIsNull()
        {
            var mockDbSet = new Mock<DbSet<FileEntity>>();
            var mockDbContext = new Mock<MyDbContext>();

            FileRepository fileRepository = new FileRepository(mockDbContext.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => fileRepository.AddFileAsync(null));
        }

        [Fact]
        public async Task GetAllFilesAsync_ReturnsEmpty_WhenNoFilesExist()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Aislado por prueba
                .Options;

            await using var context = new MyDbContext(options);
            var repo = new FileRepository(context);

            var result = await repo.GetAllFilesAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllFilesAsync_ReturnsFiles_WhenFilesExist()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using (var context = new MyDbContext(options))
            {
                context.TbFiles.AddRange(
                    new FileEntity { Id = Guid.NewGuid(), OriginalName = "file1.pdf", MimeType = "application/pdf", Path = "c:/pruebas" },
                    new FileEntity { Id = Guid.NewGuid(), OriginalName = "file2.pdf", MimeType = "application/pdf", Path = "c:/pruebas" }
                );
                await context.SaveChangesAsync();
            }

            await using (var context = new MyDbContext(options))
            {
                var repo = new FileRepository(context);
                var result = await repo.GetAllFilesAsync();

                Assert.Equal(2, result.Count());
                Assert.Contains(result, f => f.OriginalName == "file1.pdf");
                Assert.Contains(result, f => f.OriginalName == "file2.pdf");
            }
        }

        [Fact]
        public async Task GetFileByIdAsync_WhenFileNotFound()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new MyDbContext(options);
            var repo = new FileRepository(context);

            var guid = Guid.NewGuid();

            var result = await repo.GetFileByIdAsync(guid);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetFileByIdAsync_WhenFileFound()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var guidToFound = Guid.NewGuid();

            await using (var context = new MyDbContext(options))
            {
                context.TbFiles.AddRange(
                    new FileEntity { Id = guidToFound, OriginalName = "file1.pdf", MimeType = "application/pdf", Path = "c:/pruebas" }
                );
                await context.SaveChangesAsync();
            }
            ;

            await using (var context = new MyDbContext(options))
            {
                var repo = new FileRepository(context);

                var result = await repo.GetFileByIdAsync(guidToFound);

                Assert.NotNull(result);
                Assert.Contains("file1.pdf", result.OriginalName);
                Assert.Contains(guidToFound.ToString(), result.Id.ToString());
            }
            ;
        }

        [Fact]
        public async Task RemoveFileAsync_WhenDeleteIsSuccessfull()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
                
            var guidToFound = Guid.NewGuid();

            await using (var context = new MyDbContext(options))
            {
                context.TbFiles.AddRange(
                    new FileEntity { Id = guidToFound, OriginalName = "file1.pdf", MimeType = "application/pdf", Path = "c:/pruebas" }
                );
                
                await context.SaveChangesAsync();
            }
            ;

            await using (var context = new MyDbContext(options))
            {
                var repo = new FileRepository(context);
                var file = new FileEntity { Id = guidToFound, OriginalName = "file1.pdf", MimeType = "application/pdf", Path = "c:/pruebas" };
                await repo.RemoveFileAsync(file);
            }
            ;
        }
    }
}