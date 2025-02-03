using ProyectoIntegradorLibreria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs.Mapper
{
    public class ReservaMapper
    {
        public static Reserva FromDto(ReservaDTO reserva)
        {
            return new Reserva
            {
                Fecha = reserva.Fecha,
                EstadoReserva = (ProyectoIntegradorLibreria.Enum.EstadoReservaEnum)reserva.EstadoReserva,
                PedidoId = reserva.PedidoId,
                ClienteId = reserva.ClienteId,
                Camion = reserva.Camion,
                Chofer = reserva.Chofer,
                LineasReservas = reserva.LineasReservas.Select(p => new LineaReserva
                {
                    ProductoId = p.ProductoId,
                    CantidadReservada = p.CantidadReservada,

                }).ToList()
            };
        }


        public static ReservaDTO ToDto(Reserva reserva)
        {
            return new ReservaDTO()
            {
                Id = reserva.Id,
                Fecha = reserva.Fecha,
                EstadoReserva = reserva.EstadoReserva,
                PedidoId = reserva.PedidoId,
                ClienteId = reserva.ClienteId,
                Camion = reserva.Camion,
                Chofer = reserva.Chofer,
                LineasReservas = reserva.LineasReservas.Select(lr => new LineaReservaDTO
                {
                    Id = lr.Id,
                    ProductoId = lr.ProductoId,
                    Producto = lr.Producto != null
                        ? ProductoMapper.ToDto(lr.Producto) // Usa ProductoMapper para mapear correctamente
                        : new ProductoDTO { Descripcion = "Producto desconocido" }, // Valor predeterminado para productos nulos
                    CantidadReservada = lr.CantidadReservada
                }).ToList()


                /*
                Id = reserva.Id,
                Fecha = reserva.Fecha,
                EstadoReserva = reserva.EstadoReserva,
                PedidoId = reserva.PedidoId,
                ClienteId = reserva.ClienteId,
                Camion = reserva.Camion,
                Chofer = reserva.Chofer,
                LineasReservas = (List<LineaReservaDTO>)LineaReservaMapper.ToListaDto(reserva.LineasReservas)
                */
            };
        }

        public static IEnumerable<ReservaDTO> ToListaDto(IEnumerable<Reserva> reservas)
        {
            List<ReservaDTO> aux = new List<ReservaDTO>();
            foreach (var res in reservas)
            {
                ReservaDTO reservaDTO = ToDto(res);
                aux.Add(reservaDTO);
            }
            return aux;
        }
    }
}
