using UNAD.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosService _productosService;

        public ProductosController(ProductosService productosService)
        {
            _productosService = productosService;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<clsProductosBE>>> GetProductos()
        {
            return await _productosService.GetAllProductos();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<clsProductosBE>> GetProducto(int id)
        {
            try
            {
                var producto = await _productosService.GetProductoById(id);
                return Ok(producto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, clsProductosBE producto)
        {
            try
            {
                var productoUpdated = await _productosService.UpdateProducto(id, producto);
                return Ok(productoUpdated);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<clsProductosBE>> PostProducto(clsProductosBE producto)
        {
            try
            {
                var productoCreated = await _productosService.CreateProducto(producto);
                return CreatedAtAction("GetProducto", new { id = productoCreated.ProductoID }, productoCreated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                var productoDeleted = await _productosService.DeleteProducto(id);
                return Ok(productoDeleted);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}