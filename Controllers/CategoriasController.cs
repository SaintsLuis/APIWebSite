using UNAD.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriasServices _categoriasServices;

        public CategoriasController(CategoriasServices categoriasServices)
        {
            _categoriasServices = categoriasServices;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<clsCategoriasBE>>> GetCategorias()
        {
            return await _categoriasServices.GetAllCategorias();
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<clsCategoriasBE>> GetCategoria(int id)
        {
            try
            {
                var categoria = await _categoriasServices.GetCategoriaById(id);
                return Ok(categoria);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/Categorias/6
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, clsCategoriasBE categoria)
        {
            try
            {
                var categoriaUpdated = await _categoriasServices.UpdateCategoria(id, categoria);
                return Ok(categoriaUpdated);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Categorias
        [HttpPost]
        public async Task<ActionResult<clsCategoriasBE>> PostCategoria(clsCategoriasBE categoria)
        {
            try
            {
                var categoriaCreated = await _categoriasServices.CreateCategoria(categoria);
                return CreatedAtAction(nameof(GetCategoria), new { id = categoriaCreated.CategoriaID }, categoriaCreated);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {
                await _categoriasServices.DeleteCategoria(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}