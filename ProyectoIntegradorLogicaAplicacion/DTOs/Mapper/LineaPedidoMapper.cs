using ProyectoIntegradorLibreria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs.Mapper
{
    public class LineaPedidoMapper
    {
        public static LineaPedido FromDto(LineaPedidoDTO linea)
        {
            return new LineaPedido
            {
                ProductoId = linea.ProductoId,
                PresentacionId = linea.PresentacionId,
                Cantidad = linea.Cantidad,
                CantidadRestante = linea.Cantidad,
            };
        }



        public static LineaPedidoDTO ToDto(LineaPedido linea)
        {
            return new LineaPedidoDTO()
            {
                Id = linea.Id,
                ProductoId = linea.ProductoId,
                Producto = linea.Producto != null ? ProductoMapper.ToDto(linea.Producto) : new ProductoDTO { Descripcion = "Producto desconocido" },
                PresentacionId = linea.PresentacionId,
                Presentacion = linea.Presentacion != null ? new PresentacionDTO
                {
                    Id = linea.Presentacion.Id,
                    Descripcion = linea.Presentacion.Descripcion,
                    Unidad = linea.Presentacion.Unidad
                } : new PresentacionDTO { Id = 0, Descripcion = "Presentación desconocida", Unidad = "N/A" },
                Cantidad = linea.Cantidad,
                CantidadRestante = linea.CantidadRestante

                /*
                Id = linea.Id,
                ProductoId = linea.ProductoId,
                PresentacionId = linea.PresentacionId,
                Cantidad = linea.Cantidad,
                CantidadRestante = linea.Cantidad
                */
            };
        }

        public static IEnumerable<LineaPedidoDTO> ToListaDto(IEnumerable<LineaPedido> lineas)
        {
            List<LineaPedidoDTO> aux = new List<LineaPedidoDTO>();
            foreach (var linea in lineas)
            {
                LineaPedidoDTO lineaDto = ToDto(linea);
                aux.Add(lineaDto);
            }
            return aux;
        }
    }
}
