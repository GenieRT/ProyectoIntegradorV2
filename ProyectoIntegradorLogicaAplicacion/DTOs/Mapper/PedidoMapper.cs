using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs.Mapper
{
    public class PedidoMapper
    {
        public static Pedido FromDto(PedidoDTO pedido)
        {
            return new Pedido
            {
                Fecha = (DateTime)pedido.Fecha,
                Estado = (ProyectoIntegradorLibreria.Enum.EstadoPedidoEnum)pedido.Estado,
                ClienteId = pedido.ClienteId,
                Productos = pedido.Productos.Select(p => new LineaPedido
                {
                    ProductoId = p.ProductoId,
                    PresentacionId = p.PresentacionId,
                    Cantidad = p.Cantidad,
                    CantidadRestante = p.Cantidad,

                }).ToList(),
                Reservas = pedido.Reservas.Select(r => new Reserva
                {
                    Id = r.Id,
                    Fecha = r.Fecha,
                    EstadoReserva = (EstadoReservaEnum)r.EstadoReserva,
                    PedidoId = r.PedidoId,
                    ClienteId = r.ClienteId,
                    Camion = r.Camion,
                    Chofer = r.Chofer,
                    LineasReservas = r.LineasReservas.Select(lr => new LineaReserva { 
                        ProductoId = lr.ProductoId,
                        CantidadReservada = lr.CantidadReservada,
                    }).ToList()                   
                }).ToList(),
            };
        }


        public static PedidoDTO ToDto(Pedido pedido)
        {
            return new PedidoDTO()
            {
                Id = pedido.Id,
                Fecha = pedido.Fecha,
                ClienteId = pedido.ClienteId,
                Estado = pedido.Estado,
                Productos = (List<LineaPedidoDTO>)LineaPedidoMapper.ToListaDto(pedido.Productos),
                Reservas = (List<ReservaDTO>)ReservaMapper.ToListaDto(pedido.Reservas)

            };
        }
    } }
