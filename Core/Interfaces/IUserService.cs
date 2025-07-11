using Core.Entities.Dto;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterDto model);
        Task<object> LoginAsync(LoginDto model);
    }
}