using UNAD.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoFacturasController : ControllerBase
    {
        private readonly TipoFacturaService _tipoFacturaService;

        public TipoFacturasController(TipoFacturaService tipoFacturaService)
        {
            _tipoFacturaService = tipoFacturaService;
        }

        // GET: api/TipoFacturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<clsTipoFacturasBE>>> GetTipoFacturas()
        {
            return await _tipoFacturaService.GetAllTipoFacturas();
        }

        // GET: api/TipoFacturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<clsTipoFacturasBE>> GetTipoFactura(int id)
        {
            try
            {
                var tipoFactura = await _tipoFacturaService.GetTipoFacturaById(id);
                return Ok(tipoFactura);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/TipoFacturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoFactura(int id, clsTipoFacturasBE tipoFactura)
        {
            try
            {
                var tipoFacturaUpdated = await _tipoFacturaService.UpdateTipoFactura(id, tipoFactura);
                return Ok(tipoFacturaUpdated);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/TipoFacturas
        [HttpPost]
        public async Task<ActionResult<clsTipoFacturasBE>> PostTipoFactura(clsTipoFacturasBE tipoFactura)
        {
            try
            {
                var tipoFacturaCreated = await _tipoFacturaService.CreateTipoFactura(tipoFactura);
                return CreatedAtAction("GetTipoFactura", new { id = tipoFacturaCreated.TipoFacturaID }, tipoFacturaCreated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/TipoFacturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoFactura(int id)
        {
            try
            {
                var tipoFacturaDeleted = await _tipoFacturaService.DeleteTipoFactura(id);
                return Ok(tipoFacturaDeleted);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}