using Microsoft.EntityFrameworkCore;
using UNAD.DAL;
using UNAD.Entity;

namespace WebAPI.Services
{
    public class FacturasService
    {
        private readonly Context _context;

        public FacturasService(Context context)
        {
            _context = context;
        }

        public async Task<List<clsFacturasBE>> GetAllFacturas()
        {
            return await _context.clsFacturasBE.ToListAsync();
        }

        public async Task<clsFacturasBE> GetFacturaById(int id)
        {
            var factura = await _context.clsFacturasBE.FindAsync(id);
            if (factura == null)
            {
                throw new ArgumentException($"No se encontró la factura con ID {id}.", nameof(id));
            }
            return factura;
        }

        public async Task<clsFacturasBE> UpdateFactura(int id, clsFacturasBE factura)
        {
            if (id != factura.FacturaID)
            {
                throw new ArgumentException($"El ID de la factura {factura.FacturaID} no coincide con el ID de la URL {id}.", nameof(id));
            }
            _context.Entry(factura).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (FacturaExists(id))
                {
                    throw new ArgumentException($"No se encontró la factura con ID {id}.", nameof(id));
                }
                else
                {
                    throw;
                }
            }
            return factura;
        }

        public async Task<clsFacturasBE> CreateFactura(clsFacturasBE factura)
        {
            // validacion
            if (factura.ClienteID == 0)
            {
                throw new ArgumentException("El ID del cliente no puede ser 0.", nameof(factura.ClienteID));
            }
            _context.clsFacturasBE.Add(factura);
            await _context.SaveChangesAsync();
            return factura;
        }

        public async Task<clsFacturasBE> DeleteFactura(int id)
        {
            var factura = await _context.clsFacturasBE.FindAsync(id);
            if (factura == null)
            {
                throw new ArgumentException($"No se encontró la factura con ID {id}.", nameof(id));
            }
            _context.clsFacturasBE.Remove(factura);
            await _context.SaveChangesAsync();
            return factura;
        }

        public bool FacturaExists(int id)
        {
            return _context.clsClientesBE.Any(e => e.ClienteID == id);
        }

    }
}