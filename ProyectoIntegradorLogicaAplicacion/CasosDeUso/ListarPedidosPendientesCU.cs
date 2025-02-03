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
    public class ListarPedidosPendientesCU : IListarPedidosPendientes
    {
        private readonly IRepositorioPedidos repoPedidos;

        public ListarPedidosPendientesCU(IRepositorioPedidos repositorioPedidos)
        {
            this.repoPedidos = repositorioPedidos;
        }

        public IEnumerable<Pedido> listar()
        {
            try
            {
                // Llama al método del repositorio para obtener los pedidos pendientes
                return repoPedidos.ListarPedidosPendientes();
            }
            catch (ArgumentException ex)
            {
                // Lanza una excepción controlada en caso de argumentos inválidos
                throw new ArgumentException("Ocurrió un error al listar los pedidos pendientes: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Lanza una excepción general para cualquier otro error
                throw new Exception("Error inesperado al listar los pedidos pendientes: " + ex.Message);
            }
        }

    }
}
