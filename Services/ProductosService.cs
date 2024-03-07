using Microsoft.EntityFrameworkCore;
using UNAD.DAL;
using UNAD.Entity;

namespace WebAPI.Services
{
    public class ProductosService
    {
        private readonly Context _context;

        public ProductosService(Context context)
        {
            _context = context;
        }

        public async Task<List<clsProductosBE>> GetAllProductos()
        {
            return await _context.clsProductosBE.ToListAsync();
        }

        public async Task<clsProductosBE> GetProductoById(int id)
        {
            var producto = await _context.clsProductosBE.FindAsync(id);
            if (producto == null)
            {
                throw new ArgumentException($"No se encontró el producto con ID {id}.", nameof(id));
            }
            return producto;
        }

        public async Task<clsProductosBE> UpdateProducto(int id, clsProductosBE producto)
        {
            if (id != producto.ProductoID)
            {
                throw new ArgumentException($"El ID del producto {producto.ProductoID} no coincide con el ID de la URL {id}.", nameof(id));
            }
            _context.Entry(producto).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ProductosBEExists(id))
                {
                    throw new ArgumentException($"No se encontró el producto con ID {id}.", nameof(id));
                }
                else
                {
                    throw;
                }
            }
            return producto;
        }

        public async Task<clsProductosBE> CreateProducto(clsProductosBE producto)
        {
            // validacion
            if (producto.ProductoID == 0)
            {
                throw new ArgumentException("El ID del producto no puede ser 0.", nameof(producto.ProductoID));
            }

            _context.clsProductosBE.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<clsProductosBE> DeleteProducto(int id)
        {
            var producto = await _context.clsProductosBE.FindAsync(id);
            if (producto == null)
            {
                throw new ArgumentException($"No se encontró el producto con ID {id}.", nameof(id));
            }
            _context.clsProductosBE.Remove(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        private bool ProductosBEExists(int id)
        {
            return _context.clsProductosBE.Any(e => e.ProductoID == id);
        }

    }
}