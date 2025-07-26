using Core.Entities;
using Infraestructure;
using Microsoft.EntityFrameworkCore;
using Moq;

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
        public async Task AddFileAsync_Throw_WhenArgumentIsNull()
        {
            var mockDbSet = new Mock<DbSet<FileEntity>>();
            var mockDbContext = new Mock<MyDbContext>();

            FileRepository fileRepository = new FileRepository(mockDbContext.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => fileRepository.AddFileAsync(null));
        }
    }
}