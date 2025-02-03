using ProyectoIntegradorLibreria.Entities;

namespace ProyectoIntegradorLogicaAplicacion.DTOs.Mapper
{
    public static class ClientePedidoReservaMapper
    {
        public static ClientePedidoReservaDTO ToDto(IEnumerable<Pedido> pedidos, IEnumerable<Reserva> reservas)
        {
            return new ClientePedidoReservaDTO
            {
                Pedidos = pedidos.Select(p => PedidoMapper.ToDto(p)).ToList(),
                Reservas = reservas.Select(r => ReservaMapper.ToDto(r)).ToList()
            };
        }
    }
}
