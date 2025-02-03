using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.Enum;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.CasosDeUso
{
    public class AprobarPedidoCU : IAprobarPedido
    {
        private readonly IRepositorioPedidos repoPedidos;

        public AprobarPedidoCU(IRepositorioPedidos repositorioPedidos)
        {
            this.repoPedidos = repositorioPedidos;
        }

        public Empleado? BuscarEmpleadoPorId(int empleadoId)
        {
            var empleado = repoPedidos.GetClienteById(empleadoId);

            if (empleado == null || !(empleado is Empleado e)) 
            {
                return null;
            }

            return e;
        }

        public Pedido? BuscarPedidoPorId(int pedidoId)
        {
            var pedido = repoPedidos.GetPedidoById(pedidoId);

            if (pedido == null)
            {
                //throw new Exception("Pedido no encontrado.");
                return null;
            }

            return pedido;
        }
        

        public void AprobarPedidoPorId(int pedidoId)
        {
            if (pedidoId <= 0)
            {
                throw new ArgumentException("El ID del pedido debe ser mayor a 0.");
            }

            var pedido = BuscarPedidoPorId(pedidoId);
            if (pedido == null)
            {
                throw new KeyNotFoundException("Pedido no encontrado.");
            }

            if (pedido.Estado != EstadoPedidoEnum.PENDIENTE)
            {
                throw new InvalidOperationException("Solo se pueden aprobar pedidos en estado 'Pendiente'.");
            }

            pedido.Estado = EstadoPedidoEnum.APROBADO;
            repoPedidos.Update(pedido);
        }
    }
}
