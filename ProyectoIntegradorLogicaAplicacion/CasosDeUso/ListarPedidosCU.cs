using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using ProyectoIntegradorLogicaAplicacion.DTOs;
using ProyectoIntegradorLogicaAplicacion.DTOs.Mapper;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.CasosDeUso
{
    public class ListarPedidosCU : IListarPedidos
    {
        private readonly IRepositorioPedidos repoPedidos;
        private readonly IRepositorioReservas repoReservas;

        public ListarPedidosCU(IRepositorioPedidos repositorioPedidos, IRepositorioReservas repositorioReservas) 
        {
            this.repoPedidos = repositorioPedidos;
            this.repoReservas = repositorioReservas;
        }

        public Cliente? buscarClientePorId(int clienteId)
        {
            var cli = repoPedidos.GetClienteById(clienteId);

            if (cli == null || !(cli is Cliente cliente))
            {
                throw new KeyNotFoundException("Cliente no encontrado.");
            }

            return cliente;
        }


        public ClientePedidoReservaDTO ObtenerPedidosYReservasPorCliente(int clienteId)
        {
            if (clienteId <= 0)
            {
                throw new ArgumentException("El ID del cliente es inválido.");
            }

            var cliente = buscarClientePorId(clienteId);
            if (cliente == null)
            {
                throw new KeyNotFoundException("Cliente no encontrado.");
            }

            try
            {
                var pedidos = repoPedidos.GetPedidosPorCliente(clienteId) ?? new List<Pedido>();
                var reservas = repoReservas.GetReservasPorCliente(clienteId) ?? new List<Reserva>();

                return ClientePedidoReservaMapper.ToDto(pedidos, reservas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerPedidosYReservasPorCliente: {ex.Message}");
                throw new ApplicationException("Error al obtener los pedidos y reservas del cliente.", ex);
            }

        }
    }
}
