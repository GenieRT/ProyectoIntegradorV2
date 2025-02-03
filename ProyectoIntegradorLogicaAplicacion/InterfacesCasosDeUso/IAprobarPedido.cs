using ProyectoIntegradorLibreria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso
{
    public interface IAprobarPedido
    {
        Empleado? BuscarEmpleadoPorId(int empleadoId);
        Pedido? BuscarPedidoPorId(int pedidoId);
        //bool AprobarPedido(Pedido pedido);
        void AprobarPedidoPorId(int pedidoId);
    }
}
