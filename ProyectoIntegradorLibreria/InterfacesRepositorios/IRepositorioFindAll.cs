using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLibreria.InterfacesRepositorios
{
    public interface IRepositorioFindAll <T>
    {
        IEnumerable<T> FindAll();
    }
}
