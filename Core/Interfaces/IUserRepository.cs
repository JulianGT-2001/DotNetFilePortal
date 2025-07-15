using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task RegisterUserAsync(ApplicationUser user, string password);
    }
}