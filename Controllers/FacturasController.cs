using UNAD.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly FacturasService _facturasService;

        public FacturasController(FacturasService facturasService)
        {
            _facturasService = facturasService;
        }

        // GET: api/Facturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<clsFacturasBE>>> GetFacturas()
        {
            return await _facturasService.GetAllFacturas();
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<clsFacturasBE>> GetFactura(int id)
        {
            try
            {
                var factura = await _facturasService.GetFacturaById(id);
                return Ok(factura);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/Facturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(int id, clsFacturasBE factura)
        {
            try
            {
                var facturaUpdated = await _facturasService.UpdateFactura(id, factura);
                return Ok(facturaUpdated);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Facturas
        [HttpPost]
        public async Task<ActionResult<clsFacturasBE>> PostFactura(clsFacturasBE factura)
        {
            try
            {
                var facturaCreated = await _facturasService.CreateFactura(factura);
                return CreatedAtAction("GetFactura", new { id = facturaCreated.FacturaID }, facturaCreated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            try
            {
                var factura = await _facturasService.DeleteFactura(id);
                return Ok(factura);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}