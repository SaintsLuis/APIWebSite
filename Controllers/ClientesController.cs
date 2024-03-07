using UNAD.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {

        private readonly ClientesServices _clientesServices;

        public ClientesController(ClientesServices clientesServices)
        {
            _clientesServices = clientesServices;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<clsClientesBE>>> GetClientes()
        {
            return await _clientesServices.GetAllClientes();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<clsClientesBE>> GetCliente(int id)
        {
            try
            {
                var cliente = await _clientesServices.GetClienteById(id);
                return Ok(cliente);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, clsClientesBE cliente)
        {
            try
            {
                var clienteUpdated = await _clientesServices.UpdateCliente(id, cliente);
                return Ok(clienteUpdated);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<clsClientesBE>> PostCliente(clsClientesBE cliente)
        {
            try
            {
                var categoriaCreated = await _clientesServices.CreateCliente(cliente);
                return CreatedAtAction(nameof(GetCliente), new { id = categoriaCreated.ClienteID }, categoriaCreated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                var clienteDeleted = await _clientesServices.DeleteCliente(id);
                return Ok(clienteDeleted);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}