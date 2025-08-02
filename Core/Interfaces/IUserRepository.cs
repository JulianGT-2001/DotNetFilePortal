using System.Security.Claims;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task RegisterUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser?> GetUserAsync(ClaimsPrincipal claim);
        bool ResetAuthenticatorKey(ApplicationUser user);
    }
}