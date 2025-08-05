using System.Security.Claims;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task RegisterUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser?> GetUserAsync(ClaimsPrincipal claim);
        Task ResetAuthenticatorKey(ApplicationUser user);
        Task<string?> GetAuthenticatorKeyAsync(ApplicationUser user);
        Task<bool> VerifyTwoFactorTokenAsync(ApplicationUser user, string code);
        Task SetTwoFactorEnabledAsync(ApplicationUser user, bool activate);
    }
}