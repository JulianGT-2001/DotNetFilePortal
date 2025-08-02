using System.Security.Claims;
using Core.Entities;
using Core.Entities.Dto;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApplicationUser?>> ObtenerUsuario()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(user))
                return Unauthorized();

            return await _service.ObtenerUsuarioPorClaim(User);
        }

        [Authorize]
        [HttpGet("reiniciar_clave_de_autenticacion")]
        public async Task<IActionResult> ReiniciarClaveDeAutenticacion(string? email)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(user))
                return Unauthorized();

            if (string.IsNullOrEmpty(email))
                return NotFound();

            await _service.ReiniciarClaveDeAutenticacion(email);

            return Ok();
        }

        [Authorize]
        [HttpGet("obtener_clave_de_autenticacion")]
        public async Task<ActionResult<string?>> ObtenerClaveAutenticacion(string? email)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(user))
                return Unauthorized();

            if (string.IsNullOrEmpty(email))
                return NotFound();

            return await _service.ObtenerClaveDeAutenticacionAsync(email);
        }
        #endregion
    }    
}