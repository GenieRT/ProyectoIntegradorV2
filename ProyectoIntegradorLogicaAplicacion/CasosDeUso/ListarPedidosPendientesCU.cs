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
                var pedidosPendientes = repoPedidos.ListarPedidosPendientes();

                if (!pedidosPendientes.Any())
                {
                    throw new ArgumentException("No hay pedidos pendientes para aprobar.");
                }

                return pedidosPendientes;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Ocurrió un error al listar los pedidos pendientes: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al listar los pedidos pendientes: " + ex.Message);
            }
        }

    }
}
