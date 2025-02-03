using ProyectoIntegradorLibreria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLibreria.InterfacesRepositorios
{
    public interface IRepositorioPedidos : IRepositorioAdd<Pedido>, IRepositorioFindById<Pedido>, IRepositorioUpdate<Pedido>
    {

        public Usuario? GetClienteById(int clienteId);
        IEnumerable<Pedido> GetPedidosPorCliente(int clienteId);
        public Presentacion GetPresentacionById(int id);
        public Pedido? GetPedidoById(int pedidoId);
        public IEnumerable<Pedido> ListarPedidosPendientes();
        public Producto GetProductoById(int id);
        
        
    }
}
