using Moq;
using Infraestructure;
using Microsoft.AspNetCore.Identity;
using Core.Entities;

namespace fileupload.Tests
{
    public class UserRepositoryTest
    {
        [Fact]
        public void RegisterUserAsync()
        {
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            UserRepository userRepository = new UserRepository(mockUserManager.Object);
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "JulianGT",
                Email = "juliant.2001@outlook.com",
                FullName = "Julian Gonzalez"
            };
            string password = "EstaEsLaMejorSeguridad2025*.";

            var result = userRepository.RegisterUserAsync(user, password);

            mockUserManager.Verify(m => m.CreateAsync(user, password), Times.Once);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("JulianGT", null)]
        public async Task RegisterUserAsync_Throws_WhenDataIsInvalid(string userName, string Email)
        {
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            mockUserManager.Setup(t => t.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ThrowsAsync(new ArgumentNullException("username"));
            UserRepository userRepository = new UserRepository(mockUserManager.Object);
            ApplicationUser user = new ApplicationUser()
            {
                UserName = userName,
                Email = Email,
                FullName = "Julian Gonzalez"
            };

            await Assert.ThrowsAsync<ArgumentNullException>(() => userRepository.RegisterUserAsync(user, "password"));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("JulianGT", null)]
        public async Task RegisterUserAsync_Throws_WhenDataIsValid(string userName, string Email)
        {
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            UserRepository userRepository = new UserRepository(mockUserManager.Object);
            ApplicationUser user = new ApplicationUser()
            {
                UserName = userName,
                Email = Email,
                FullName = "Julian Gonzalez"
            };

            await userRepository.RegisterUserAsync(user, "password");
        }

        [Fact]
        public async Task RegisterUserAsync_Throws_WhenUserManagerThrows()
        {
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
            );
            mockUserManager.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("Error"));
            UserRepository userRepository = new UserRepository(mockUserManager.Object);

            await Assert.ThrowsAsync<Exception>(() => userRepository.RegisterUserAsync(new ApplicationUser(), "password"));
        }
    }
}