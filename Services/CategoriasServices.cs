using Microsoft.EntityFrameworkCore;
using UNAD.DAL;
using UNAD.Entity;

namespace WebAPI.Services
{
    public class CategoriasServices
    {

        private readonly Context _context;

        public CategoriasServices(Context context)
        {
            _context = context;
        }

        // Todas las categorias
        public async Task<List<clsCategoriasBE>> GetAllCategorias()
        {
            return await _context.clsCategoriasBE.ToListAsync();
        }

        public async Task<clsCategoriasBE> GetCategoriaById(int id)
        {
            var categoria = await _context.clsCategoriasBE.FindAsync(id);
            if (categoria == null)
            {
                throw new ArgumentException($"No se encontró la categoría con ID {id}.", nameof(id));
            }
            return categoria;
        }

        public async Task<clsCategoriasBE> UpdateCategoria(int id, clsCategoriasBE categoria)
        {
            if (id != categoria.CategoriaID)
            {
                throw new ArgumentException($"El ID de la categoría {categoria.CategoriaID} no coincide con el ID de la URL {id}.", nameof(id));
            }
            _context.Entry(categoria).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (CategoriasBEExists(id))
                {
                    throw new ArgumentException($"No se encontró la categoría con ID {id}.", nameof(id));
                }
                else
                {
                    throw;
                }
            }
            return categoria;
        }

        public async Task<clsCategoriasBE> CreateCategoria(clsCategoriasBE categoria)
        {
            // validacion
            if (categoria == null)
            {
                throw new ArgumentException("La categoría no puede ser nula.", nameof(categoria));

            }

            // Si la categoria ya existe
            if (_context.clsCategoriasBE.Any(e => e.CategoriaID == categoria.CategoriaID))
            {
                throw new ArgumentException($"La categoría con ID {categoria.CategoriaID} ya existe.", nameof(categoria.CategoriaID));
            }

            _context.clsCategoriasBE.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<clsCategoriasBE> DeleteCategoria(int id)
        {
            var categoria = await _context.clsCategoriasBE.FindAsync(id);
            if (categoria == null)
            {
                throw new ArgumentException($"No se encontró la categoría con ID {id}.", nameof(id));
            }

            _context.clsCategoriasBE.Remove(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public bool CategoriasBEExists(int id)
        {
            return _context.clsCategoriasBE.Any(e => e.CategoriaID == id);
        }

    }
}