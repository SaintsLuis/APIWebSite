using Microsoft.EntityFrameworkCore;
using UNAD.DAL;
using UNAD.Entity;

namespace WebAPI.Services
{
    public class TipoFacturaService
    {
        private readonly Context _context;

        public TipoFacturaService(Context context)
        {
            _context = context;
        }

        public async Task<List<clsTipoFacturasBE>> GetAllTipoFacturas()
        {
            return await _context.clsTipoFacturasBE.ToListAsync();
        }

        public async Task<clsTipoFacturasBE> GetTipoFacturaById(int id)
        {
            var tipoFactura = await _context.clsTipoFacturasBE.FindAsync(id);
            if (tipoFactura == null)
            {
                throw new ArgumentException($"No se encontró el tipo de factura con ID {id}.", nameof(id));
            }
            return tipoFactura;
        }

        public async Task<clsTipoFacturasBE> UpdateTipoFactura(int id, clsTipoFacturasBE tipoFactura)
        {
            if (id != tipoFactura.TipoFacturaID)
            {
                throw new ArgumentException($"El ID del tipo de factura {tipoFactura.TipoFacturaID} no coincide con el ID de la URL {id}.", nameof(id));
            }
            _context.Entry(tipoFactura).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (TipoFacturaExists(id))
                {
                    throw new ArgumentException($"No se encontró el tipo de factura con ID {id}.", nameof(id));
                }
                else
                {
                    throw;
                }
            }
            return tipoFactura;
        }

        public async Task<clsTipoFacturasBE> CreateTipoFactura(clsTipoFacturasBE tipoFactura)
        {
            // validacion
            if (tipoFactura.TipoFacturaID == 0)
            {
                throw new ArgumentException("El ID del tipo de factura no puede ser 0.", nameof(tipoFactura.TipoFacturaID));
            }
            _context.clsTipoFacturasBE.Add(tipoFactura);
            await _context.SaveChangesAsync();
            return tipoFactura;
        }

        public async Task<clsTipoFacturasBE> DeleteTipoFactura(int id)
        {
            var tipoFactura = await _context.clsTipoFacturasBE.FindAsync(id);
            if (tipoFactura == null)
            {
                throw new ArgumentException($"No se encontró el tipo de factura con ID {id}.", nameof(id));
            }
            _context.clsTipoFacturasBE.Remove(tipoFactura);
            await _context.SaveChangesAsync();
            return tipoFactura;
        }

        private bool TipoFacturaExists(int id)
        {
            return _context.clsTipoFacturasBE.Any(e => e.TipoFacturaID == id);
        }

    }
}