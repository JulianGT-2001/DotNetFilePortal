using System.Security.Claims;
using Core.Entities;
using Core.Entities.Dto;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterDto model);
        object LoginAsync(ApplicationUser user);
        Task<ApplicationUser?> ObtenerUsuarioPorClaim(ClaimsPrincipal claim);
        Task ReiniciarClaveDeAutenticacion(string email);
        Task<string?> ObtenerClaveDeAutenticacionAsync(string email);
    }
}