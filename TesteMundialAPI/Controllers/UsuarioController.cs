using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TesteMundialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _usuarioService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Usuario/<id>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _usuarioService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioDTO usuario)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            usuario.Perfil = null;
            var usuarioExistente = await _usuarioService.GetByEmailAndSenhaAsync(usuario.Email, usuario.Senha);

            if (usuarioExistente != null)
            {
                return BadRequest("Email já cadastrado");
            }

            await _usuarioService.AddAsync(usuario);

            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UsuarioDTO usuario)
        {
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = _usuarioService.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            await _usuarioService.DeleteAsync(id);
            return NoContent();
        }
    }
}
