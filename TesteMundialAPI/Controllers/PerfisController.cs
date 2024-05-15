using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TesteMundialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PerfisController : Controller
    {
        private readonly IPerfilService _perfilService;

        public PerfisController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _perfilService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Perfis/<id>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _perfilService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PerfilDTO perfil)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var perfilExistente = await _perfilService.GetByNomeAsync(perfil.Nome);

            if (perfilExistente != null)
            {
                return BadRequest("Perfil já cadastrado");
            }

            await _perfilService.AddAsync(perfil);

            return CreatedAtAction(nameof(GetById), new { id = perfil.IdPerfil }, perfil);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PerfilDTO perfil)
        {
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var perfil = await _perfilService.GetByIdAsync(id);

            if (perfil == null)
            {
                return NotFound();
            }

            await _perfilService.Delete(id);
            return Ok();
        }
    }
}
