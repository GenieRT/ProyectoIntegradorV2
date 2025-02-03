using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorAccesData.EntityFramework.SQL
{
    public class RepositorioPresentaciones : IRepositorioPresentaciones
    {
        private ISUSAContext _context;

        public RepositorioPresentaciones()
        {
            _context = new ISUSAContext();
        }

        public IEnumerable<Presentacion> FindAll()
        {
            return _context.Presentacions;
        }

    }
}
