using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace TesteMundialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthDTO auth)
        {
            var token = await _usuarioService.AuthenticateAsync(auth);

            if(token == null)
            {
                return Unauthorized();
            }

            return Ok(new { Token = token });
        }
    }
}
