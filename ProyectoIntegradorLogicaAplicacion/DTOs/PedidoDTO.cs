using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public EstadoPedidoEnum? Estado { get; set; }
        public List<LineaPedidoDTO> Productos { get; set; }
        public ClienteDTO ? Cliente { get; set; }

        public List<ReservaDTO>? Reservas { get; set; }

        public int ClienteId { get; set; }

        public PedidoDTO() 
        { 
           this.Productos = new List<LineaPedidoDTO>();
           this.Reservas = new List<ReservaDTO>();
           this.Estado = EstadoPedidoEnum.PENDIENTE;
           this.Fecha = DateTime.Now;
        }
        public PedidoDTO(Pedido pedido)
        {
            if (pedido == null)
            {
                throw new ArgumentNullException(nameof(pedido), "El objeto Pedido es nulo.");
            }

            this.Id = pedido.Id;
           this.Fecha = pedido.Fecha;
           this.ClienteId = pedido.ClienteId;
           this.Cliente = new ClienteDTO(pedido.Cliente);
           this.Estado = pedido.Estado;

            Productos = pedido.Productos?.Select(lp => new LineaPedidoDTO
            {
                Id = lp.Id,
                ProductoId = lp.ProductoId,
                Producto = lp.Producto != null ? new ProductoDTO(lp.Producto) : null, // Manejar producto nulo
                PresentacionId = lp.PresentacionId, // Usar directamente PresentacionId
                Presentacion = lp.Presentacion != null ? new PresentacionDTO(lp.Presentacion)
                                           : new PresentacionDTO { Id = 0, Descripcion = "Sin Presentación", Unidad = "N/A" }, // Valor predeterminado
                Cantidad = lp.Cantidad,
                CantidadRestante = lp.CantidadRestante
            }).ToList() ?? new List<LineaPedidoDTO>(); // Manejar lista nula

            Reservas = pedido.Reservas?.Select(r => new ReservaDTO(r)).ToList() ?? new List<ReservaDTO>(); // Manejar lista nula


            if (pedido?.Productos != null) 
            {
                this.Productos = pedido.Productos.Select(lp => new LineaPedidoDTO(lp)).ToList();
            }
            if (pedido?.Reservas != null)
            {
                this.Reservas = pedido.Reservas.Select(r => new ReservaDTO(r)).ToList();
            }
        }
    }
}
 