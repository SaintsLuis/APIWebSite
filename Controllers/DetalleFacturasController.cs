using UNAD.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleFacturasController : ControllerBase
    {
        private readonly DetalleFacturaService _detalleFacturaService;

        public DetalleFacturasController(DetalleFacturaService detalleFacturaService)
        {
            _detalleFacturaService = detalleFacturaService;
        }

        // GET: api/DetalleFacturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<clsDetalleFacturasBE>>> GetDetalleFacturas()
        {
            return await _detalleFacturaService.GetAllDetalleFacturas();
        }

        // GET: api/DetalleFacturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<clsDetalleFacturasBE>> GetDetalleFactura(int id)
        {
            try
            {
                var detalleFactura = await _detalleFacturaService.GetDetalleFacturaById(id);
                return Ok(detalleFactura);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/DetalleFacturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleFactura(int id, clsDetalleFacturasBE detalleFactura)
        {
            try
            {
                var detalleFacturaUpdated = await _detalleFacturaService.UpdateDetalleFactura(id, detalleFactura);
                return Ok(detalleFacturaUpdated);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/DetalleFacturas
        [HttpPost]
        public async Task<ActionResult<clsDetalleFacturasBE>> PostDetalleFactura(clsDetalleFacturasBE detalleFactura)
        {
            try
            {
                var detalleFacturaCreated = await _detalleFacturaService.CreateDetalleFactura(detalleFactura);
                return CreatedAtAction("GetDetalleFactura", new { id = detalleFacturaCreated.DetalleFacturaID }, detalleFacturaCreated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/DetalleFacturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleFactura(int id)
        {
            try
            {
                var detalleFactura = await _detalleFacturaService.DeleteDetalleFactura(id);
                return Ok(detalleFactura);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }



    }
}