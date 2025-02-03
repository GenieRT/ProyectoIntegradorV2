using Microsoft.EntityFrameworkCore;
using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorAccesData.EntityFramework.SQL
{
    public class RepositorioPedidos : IRepositorioPedidos
    {
        private ISUSAContext _context;

        public RepositorioPedidos()
        {
            _context = new ISUSAContext();
        }
        public void Add(Pedido pedido)
        {
            try
            {

                if (pedido == null)
                {
                    throw new ArgumentNullException(nameof(pedido), "El pedido no puede ser nulo.");
                }
                if(GetClienteById(pedido.ClienteId) == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                foreach(LineaPedido ln in pedido.Productos)
                {
                    if(GetProductoById(ln.ProductoId) == null || GetPresentacionById(ln.PresentacionId) == null){
                        throw new Exception("Producto o presentación no encontrado");
                    }
                }

                
                pedido.Validar();
                _context.Pedidos.Add(pedido);
                // Guardar cambios
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al hacer pedido: " + ex.Message);
            }
        }

        public Usuario? GetClienteById(int id)
        {
            return _context.Usuarios.FirstOrDefault(c => c.Id == id);
        }

        public Pedido? GetPedidoById(int pedidoId)
        {
            return _context.Pedidos
                           .Include(p => p.Productos) // Incluye los productos relacionados
                           .FirstOrDefault(p => p.Id == pedidoId);
        }

        public void Update(Pedido p)
        {
            _context.Pedidos.Update(p);
            _context.SaveChanges();
        }

        public Presentacion GetPresentacionById(int id)
        {
            return _context.Presentacions.FirstOrDefault(p => p.Id == id);
        }


        public Pedido FindByID(int id)
        {
            return _context.Pedidos.Where(ped => ped.Id == id).FirstOrDefault();
        }

        public Producto GetProductoById(int id)
        {
            return _context.Productos.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Pedido> GetPedidosPorCliente(int clienteId)
        {
            try
            {
                Console.WriteLine($"Inicio del repositorio: GetPedidosPorCliente para ClienteId: {clienteId}");
                var pedidos = _context.Pedidos
                .Include(p => p.Cliente) // Incluye la información del cliente
                .Include(p => p.Productos)
                    .ThenInclude(lp => lp.Producto)
                .Include(p => p.Productos)
                    .ThenInclude(lp => lp.Presentacion)
                .Include(p => p.Reservas)
                    .ThenInclude(r => r.LineasReservas)
                        .ThenInclude(lr => lr.Producto)
                .Where(p => p.ClienteId == clienteId)
                .ToList();

                Console.WriteLine($"Cantidad de pedidos obtenidos: {pedidos.Count}");

                return pedidos;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetPedidosPorCliente: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Detalle interno: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        public IEnumerable<Pedido> ListarPedidosPendientes()
        {
            try
            {
                return _context.Pedidos.Where(p => p.Estado == 0).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al listar los pedidos pendientes: " + ex.Message);
            }
        }

    }
}
