using Microsoft.EntityFrameworkCore;
using UNAD.DAL;
using UNAD.Entity;

namespace WebAPI.Services
{
    public class DetalleFacturaService
    {
        private readonly Context _context;

        public DetalleFacturaService(Context context)
        {
            _context = context;
        }

        public async Task<List<clsDetalleFacturasBE>> GetAllDetalleFacturas()
        {
            return await _context.clsDetalleFacturasBE.ToListAsync();
        }

        public async Task<clsDetalleFacturasBE> GetDetalleFacturaById(int id)
        {
            var detalleFactura = await _context.clsDetalleFacturasBE.FindAsync(id);
            if (detalleFactura == null)
            {
                throw new ArgumentException($"No se encontró el detalle de factura con ID {id}.", nameof(id));
            }
            return detalleFactura;
        }

        public async Task<clsDetalleFacturasBE> UpdateDetalleFactura(int id, clsDetalleFacturasBE detalleFactura)
        {
            if (id != detalleFactura.DetalleFacturaID)
            {
                throw new ArgumentException($"El ID del detalle de factura {detalleFactura.DetalleFacturaID} no coincide con el ID de la URL {id}.", nameof(id));
            }
            _context.Entry(detalleFactura).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (DetalleFacturaExists(id))
                {
                    throw new ArgumentException($"No se encontró el detalle de factura con ID {id}.", nameof(id));
                }
                else
                {
                    throw;
                }
            }
            return detalleFactura;
        }

        public async Task<clsDetalleFacturasBE> CreateDetalleFactura(clsDetalleFacturasBE detalleFactura)
        {

            // validacion
            if (detalleFactura.FacturaID == 0)
            {
                throw new ArgumentException("El ID de la factura es requerido.", nameof(detalleFactura.FacturaID));
            }

            _context.clsDetalleFacturasBE.Add(detalleFactura);
            await _context.SaveChangesAsync();
            return detalleFactura;
        }

        public async Task<clsDetalleFacturasBE> DeleteDetalleFactura(int id)
        {
            var detalleFactura = await _context.clsDetalleFacturasBE.FindAsync(id);
            if (detalleFactura == null)
            {
                throw new ArgumentException($"No se encontró el detalle de factura con ID {id}.", nameof(id));
            }
            _context.clsDetalleFacturasBE.Remove(detalleFactura);
            await _context.SaveChangesAsync();
            return detalleFactura;
        }

        private bool DetalleFacturaExists(int id)
        {
            return _context.clsDetalleFacturasBE.Any(e => e.DetalleFacturaID == id);
        }

    }
}