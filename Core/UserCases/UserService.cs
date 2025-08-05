using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Core.Entities.Dto;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.UserCases
{
    public class UserService : IUserService
    {
        #region Atributos
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        #region Constructor
        public UserService(IConfiguration configuration, IUserRepository repository, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _repository = repository;
            _userManager = userManager;
        }

        #endregion

        #region Metodos
        public async Task RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            await _repository.RegisterUserAsync(user, model.Password);
        }

        public object LoginAsync(ApplicationUser user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            var jwtSettings = _configuration.GetSection("Jwt");
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["DurationInMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
            );

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
        }

        public async Task<ApplicationUser?> ObtenerUsuarioPorClaim(ClaimsPrincipal claim)
        {
            return await _repository.GetUserAsync(claim);
        }

        public async Task ReiniciarClaveDeAutenticacion(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
                await _repository.ResetAuthenticatorKey(user);
        }

        public async Task<string?> ObtenerClaveDeAutenticacionAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return string.Empty;
            return await _repository.GetAuthenticatorKeyAsync(user);
        }

        public async Task<bool> VerificarTokenDosFactoresAsync(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            return await _repository.VerifyTwoFactorTokenAsync(user, code);
        }

        public async Task HabilitarDosFactoresAsync(string email, bool activate)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _repository.SetTwoFactorEnabledAsync(user, activate);
            }
        }
        #endregion
    }
}