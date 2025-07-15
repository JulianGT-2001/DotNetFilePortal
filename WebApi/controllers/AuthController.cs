using Core.Entities;
using Core.Entities.Dto;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Atributos
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _service;
        #endregion

        #region Constructor
        public AuthController(UserManager<ApplicationUser> userManager, IUserService service)
        {
            _userManager = userManager;
            _service = service;
        }
        #endregion

        #region Metodos
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            await _service.RegisterAsync(model);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized();
            }

            var result = _service.LoginAsync(user);

            return Ok(result);
        }
        #endregion
    }    
}