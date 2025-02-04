using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLogicaAplicacion.DTOs.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs
{
    public class ClientePedidoReservaDTO
    {
        public List<PedidoDTO> Pedidos { get; set; }
        public List<ReservaDTO> Reservas { get; set; }

        public ClientePedidoReservaDTO()
        {
            Pedidos = new List<PedidoDTO>();
            Reservas = new List<ReservaDTO>();
        }

        public ClientePedidoReservaDTO(IEnumerable<Pedido> pedidos, IEnumerable<Reserva> reservas)
        {
            Pedidos = pedidos?.Select(p => PedidoMapper.ToDto(p)).ToList() ?? new List<PedidoDTO>();
            Reservas = reservas?.Select(r => ReservaMapper.ToDto(r)).ToList() ?? new List<ReservaDTO>();
        }



    }
}
