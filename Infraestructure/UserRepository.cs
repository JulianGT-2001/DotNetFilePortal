using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infraestructure
{
    public class UserRepository : IUserRepository
    {
        #region Atributos
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        #region Constructor
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        #endregion

        #region Metodos
        public async Task RegisterUserAsync(ApplicationUser user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }
        #endregion
    }
}