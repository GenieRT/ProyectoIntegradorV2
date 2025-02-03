using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorAccesData.EntityFramework.SQL
{
    public class RepositorioProductos : IRepositorioProductos
    {

        private ISUSAContext _context;

        public RepositorioProductos()
        {
            _context = new ISUSAContext();
        }

        public IEnumerable<Producto> FindAll()
        {
            return _context.Productos.OrderBy(p => p.Descripcion);
        }

        public Producto FindByID(int id)
        {
            return _context.Productos.FirstOrDefault(p => p.Id == id);
        }

    }
}
