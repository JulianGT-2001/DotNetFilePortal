using Core.Entities;
using Core.Entities.Dto;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterDto model);
        object LoginAsync(ApplicationUser user);
    }
}