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



        /*
        public ClientePedidoReservaDTO(IEnumerable<Pedido> pedidos, IEnumerable<Reserva> reservas)
        {
            //Pedidos = pedidos.Select(p => new PedidoDTO(p)).ToList();
            //Reservas = reservas.Select(r => new ReservaDTO(r)).ToList();

            
            // Validación para pedidos
            Pedidos = pedidos?.Select(p => new PedidoDTO
             {
                 Id = p.Id,
                 Fecha = p.Fecha,
                 Estado = p.Estado,
                 ClienteId = p.ClienteId,
                 Cliente = p.Cliente != null ? new ClienteDTO(p.Cliente) : null, // Manejar cliente nulo
                 Productos = p.Productos?.Select(lp => new LineaPedidoDTO
                 {
                     Id = lp.Id,
                     ProductoId = lp.ProductoId,
                     Producto = lp.Producto != null ? new ProductoDTO(lp.Producto) : null, // Manejar producto nulo
                     PresentacionId = lp.PresentacionId, // Asignar un valor predeterminado si es null
                     Presentacion = lp.Presentacion != null ? new PresentacionDTO(lp.Presentacion)
                                            : new PresentacionDTO { Id = 0, Descripcion = "Sin Presentación", Unidad = "N/A" },
                     Cantidad = lp.Cantidad,
                     CantidadRestante = lp.CantidadRestante
                 }).ToList() ?? new List<LineaPedidoDTO>(), // Manejar lista de productos nula
                 //Reservas = p.Reservas?.Select(r => new ReservaDTO(r)).ToList() ?? new List<ReservaDTO>() // Manejar lista de reservas nula
             }).ToList() ?? new List<PedidoDTO>(); // Manejar lista de pedidos nula

             // Validación para reservas
             Reservas = reservas?.Select(r => new ReservaDTO
             {
                 Id = r.Id,
                 Fecha = r.Fecha,
                 EstadoReserva = r.EstadoReserva,
                 PedidoId = r.PedidoId,
                 Pedido = r.Pedido != null ? new PedidoDTO(r.Pedido) : null, // Manejar pedido nulo
                 ClienteId = r.ClienteId,
                 Cliente = r.Cliente != null ? new ClienteDTO(r.Cliente) : null, // Manejar cliente nulo
                 Camion = r.Camion,
                 Chofer = r.Chofer,
                 LineasReservas = r.LineasReservas?.Select(lr => new LineaReservaDTO
                 {
                     Id = lr.Id,
                     ProductoId = lr.ProductoId,
                     Producto = lr.Producto != null ? new ProductoDTO(lr.Producto) : null, // Manejar producto nulo
                     CantidadReservada = lr.CantidadReservada
                 }).ToList() ?? new List<LineaReservaDTO>() // Manejar lista de líneas de reserva nula
             }).ToList() ?? new List<ReservaDTO>(); // Manejar lista de reservas nula
         } 
        */


    }
}
