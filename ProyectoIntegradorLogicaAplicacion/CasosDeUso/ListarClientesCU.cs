using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.CasosDeUso
{
    public class ListarClientesCU : IListarClientes
    {
        private readonly IRepositorioUsuarios _repoUsuarios;

        public ListarClientesCU(IRepositorioUsuarios repoUsuarios)
        {
            _repoUsuarios = repoUsuarios;
        }

        public IEnumerable<Cliente> Listar()
        {
                // Validar que el repositorio esté disponible
                if (_repoUsuarios == null)
                {
                    throw new InvalidOperationException("El repositorio de usuarios no está disponible.");
                }

                var clientes = _repoUsuarios.GetAllClientes();

                if (clientes == null)
                {
                    throw new Exception("No se pudo obtener la lista de clientes.");
                }

                if (!clientes.Any())
                {
                    throw new Exception("No hay clientes registrados en el sistema.");
                }

                return clientes;
          
        }
    }
}
