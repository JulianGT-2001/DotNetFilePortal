using Core.Entities;
using Core.Interfaces;
using Core.UserCases;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace fileupload.Tests
{
    public class FileServiceTest
    {
        [Fact]
        public async Task RegisterFileAsync_ShouldSaveFilesAndReturnGuids()
        {
            // Arrange
            var mockRepository = new Mock<IFileRepository>();
            var mockConfig = new Mock<IConfiguration>();
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            mockConfig.Setup(c => c["DownloadPath"]).Returns(tempPath);

            // Prepara archivo falso
            var fileName = "test.txt";
            var content = "Hello world";
            var fileMock = new Mock<IFormFile>();
            var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);
            var stream = new MemoryStream(contentBytes);

            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(stream.Length);
            fileMock.Setup(f => f.ContentType).Returns("text/plain");
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default))
                .Returns<Stream, CancellationToken>((target, token) => stream.CopyToAsync(target));

            var files = new FormFileCollection { fileMock.Object };

            var expectedGuids = new List<Guid> { Guid.NewGuid() };
            mockRepository.Setup(r => r.AddFileAsync(It.IsAny<List<FileEntity>>()))
                .ReturnsAsync(expectedGuids);

            var service = new FileService(mockRepository.Object, mockConfig.Object);

            // Act
            var result = await service.RegisterFileAsync(files, "user123");

            Assert.Equal(expectedGuids, result);

            // Cleanup (borra archivos creados)
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, recursive: true);
            }
        }
    }
}