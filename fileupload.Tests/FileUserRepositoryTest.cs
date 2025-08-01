using Core.Entities;
using Core.Entities.Dto;
using Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace fileupload.Tests
{
    public class FileUserRepositoryTest
    {
        [Fact]
        public async Task AddFileUserAsync_GuidListIsNull()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            await using var context = new MyDbContext(options);
            List<Guid> guids = new List<Guid>();
            var repo = new FileUserRepository(context);

            await repo.AddFileUserAsync("guid", guids);
        }

        [Fact]
        public async Task AddFileUserAsync_ShouldAddFileUsers_WhenGuidsAreProvided()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new MyDbContext(options);
            var repo = new FileUserRepository(context);

            var userId = "test-user-id";
            var guids = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            // Act
            await repo.AddFileUserAsync(userId, guids);

            // Assert
            var fileUsersInDb = context.TbFilesUser.ToList();

            Assert.Equal(3, fileUsersInDb.Count);
            foreach (var guid in guids)
            {
                Assert.Contains(fileUsersInDb, fu => fu.FileId == guid && fu.UserId == userId);
            }
        }

        [Fact]
        public async Task GetAllFilesUserAsync_ShouldFoundFiles_WhenUserHasFiles()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var fileGuid = Guid.NewGuid();
            string userId = Guid.NewGuid().ToString();

            await using (var context = new MyDbContext(options))
            {
                context.AddRange(
                    new FileEntity { Id = fileGuid, OriginalName = "file1.pdf", MimeType = "application/pdf", Path = "c:/pruebas" }
                );

                context.AddRange(
                    new FileUser { Id = Guid.NewGuid(), FileId = fileGuid, UserId = userId }
                );

                await context.SaveChangesAsync();
            }

            await using (var context = new MyDbContext(options))
            {
                var repo = new FileUserRepository(context);

                var result = await repo.GetAllFilesUserAsync(userId);

                Assert.Equal(1, result.Count());

                var userFiles = context.TbFilesUser.ToList();

                Assert.Equal(1, userFiles.Count());

                foreach (var file in userFiles)
                {
                    Assert.Contains(file.FileId.ToString(), fileGuid.ToString());
                    Assert.Contains(userId, file.UserId);
                }
            }
            ;
        }

        [Fact]
        public async Task GetAllFilesUserAsync_ShouldntFoundFiles_WhenUserHasNotFiles()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var fileGuid = Guid.NewGuid();
            string userId = Guid.NewGuid().ToString();

            await using (var context = new MyDbContext(options))
            {
                context.AddRange(
                    new FileEntity { Id = fileGuid, OriginalName = "file1.pdf", MimeType = "application/pdf", Path = "c:/pruebas" }
                );

                context.AddRange(
                    new FileUser { Id = Guid.NewGuid(), FileId = fileGuid, UserId = Guid.NewGuid().ToString() }
                );

                await context.SaveChangesAsync();
            }

            await using (var context = new MyDbContext(options))
            {
                var repo = new FileUserRepository(context);

                var result = await repo.GetAllFilesUserAsync(userId);

                Assert.Equal(0, result.Count());

                var userFiles = context.TbFilesUser.ToList();

                foreach (var file in userFiles)
                {
                    Assert.DoesNotContain(userId, file.UserId);
                }
            }
            ;
        }

        [Fact]
        public async Task GetFileByUserIdAsync_ShouldFoundFile_WhenUserHasFile()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var fileGuid = Guid.NewGuid();
            string userId = Guid.NewGuid().ToString();

            await using (var context = new MyDbContext(options))
            {
                context.AddRange(
                    new FileEntity { Id = fileGuid, OriginalName = "file1.pdf", MimeType = "application/pdf", Path = "c:/pruebas" }
                );

                context.AddRange(
                    new FileUser { Id = Guid.NewGuid(), FileId = fileGuid, UserId = userId }
                );

                await context.SaveChangesAsync();
            }

            await using (var context = new MyDbContext(options))
            {
                var repo = new FileUserRepository(context);

                var result = await repo.GetFileByUserIdAsync(userId, fileGuid);

                Assert.NotNull(result);
                Assert.Equal(result.Id, fileGuid);
            }
            ;
        }

        [Fact]
        public async Task GetFileByUserIdAsync_ShouldntFoundFile_WhenUserHasNotFile()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var fileGuid = Guid.NewGuid();
            string userId = Guid.NewGuid().ToString();

            await using (var context = new MyDbContext(options))
            {
                context.AddRange(
                    new FileEntity { Id = Guid.NewGuid(), OriginalName = "file1.pdf", MimeType = "application/pdf", Path = "c:/pruebas" }
                );

                context.AddRange(
                    new FileUser { Id = Guid.NewGuid(), FileId = fileGuid, UserId = Guid.NewGuid().ToString() }
                );

                await context.SaveChangesAsync();
            }

            await using (var context = new MyDbContext(options))
            {
                var repo = new FileUserRepository(context);

                var result = await repo.GetFileByUserIdAsync(userId, fileGuid);

                Assert.Null(result);
            }
            ;
        }
        
        [Fact]
        public async Task RemoveFileByUserIdAsync_ShouldRemoveFile_WhenUserWantDeleteFile()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var fileGuid = Guid.NewGuid();
            string userId = Guid.NewGuid().ToString();

            await using (var context = new MyDbContext(options))
            {
                context.AddRange(
                    new FileEntity { Id = fileGuid, OriginalName = "file1.pdf", MimeType = "application/pdf", Path = "c:/pruebas" }
                );

                context.AddRange(
                    new FileUser { Id = Guid.NewGuid(), FileId = fileGuid, UserId = userId }
                );

                await context.SaveChangesAsync();
            }

            var fileDto = new FileResponseDto
            { Id = fileGuid, OriginalName = "file1.pdf", MimeType = "application/pdf", Path = "c:/pruebas" };

            await using (var context = new MyDbContext(options))
            {
                var repo = new FileUserRepository(context);

                await repo.RemoveFileByUserIdAsync(userId, fileDto);

                var fileUserFilesList = await context.TbFilesUser
                    .FirstOrDefaultAsync(t => t.UserId == userId && t.FileId == fileGuid);

                Assert.Null(fileUserFilesList);
            }
            ;
        }
    }
}