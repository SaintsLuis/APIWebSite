using Microsoft.EntityFrameworkCore;
using UNAD.DAL;
using UNAD.Entity;

namespace WebAPI.Services
{
    public class ClientesServices
    {

        private readonly Context _context;

        public ClientesServices(Context context)
        {
            _context = context;
        }

        // Todos los clientes
        public async Task<List<clsClientesBE>> GetAllClientes()
        {
            return await _context.clsClientesBE.ToListAsync();
        }

        public async Task<clsClientesBE> GetClienteById(int id)
        {
            var cliente = await _context.clsClientesBE.FindAsync(id);
            if (cliente == null)
            {
                throw new ArgumentException($"No se encontró el cliente con ID {id}.", nameof(id));
            }
            return cliente;
        }

        public async Task<clsClientesBE> UpdateCliente(int id, clsClientesBE cliente)
        {
            if (id != cliente.ClienteID)
            {
                throw new ArgumentException($"El ID del cliente {cliente.ClienteID} no coincide con el ID de la URL {id}.", nameof(id));
            }
            _context.Entry(cliente).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ClientesBEExists(id))
                {
                    throw new ArgumentException($"No se encontró el cliente con ID {id}.", nameof(id));
                }
                else
                {
                    throw;
                }
            }
            return cliente;
        }

        public async Task<clsClientesBE> CreateCliente(clsClientesBE cliente)
        {
            // validacion
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }

            _context.clsClientesBE.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<clsClientesBE> DeleteCliente(int id)
        {
            var cliente = await _context.clsClientesBE.FindAsync(id);
            if (cliente == null)
            {
                throw new ArgumentException($"No se encontró el cliente con ID {id}.", nameof(id));
            }
            _context.clsClientesBE.Remove(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        private bool ClientesBEExists(int id)
        {
            return _context.clsClientesBE.Any(e => e.ClienteID == id);
        }

    }
}