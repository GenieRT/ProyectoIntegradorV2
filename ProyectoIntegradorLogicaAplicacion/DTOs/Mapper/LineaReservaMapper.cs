using ProyectoIntegradorLibreria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs.Mapper
{
    public class LineaReservaMapper
    {
        public static LineaReserva FromDto(LineaReservaDTO linea)
        {
            return new LineaReserva
            {
                ProductoId = linea.ProductoId,
                CantidadReservada = linea.CantidadReservada
            };
        }



        public static LineaReservaDTO ToDto(LineaReserva linea)
        {
            return new LineaReservaDTO()
            {
                Id = linea.Id,
                ProductoId = linea.ProductoId,
                CantidadReservada = linea.CantidadReservada

            };
        }

        public static IEnumerable<LineaReservaDTO> ToListaDto(IEnumerable<LineaReserva> lineas)
        {
            List<LineaReservaDTO> aux = new List<LineaReservaDTO>();
            foreach (var linea in lineas)
            {
                LineaReservaDTO lineaDto = ToDto(linea);
                aux.Add(lineaDto);
            }
            return aux;
        }

        public static ProductoDemandaDTO MapToProductoDemandaDTO(IEnumerable<LineaReserva> lineasReserva, Producto producto)
        {
            var toneladasReservadas = lineasReserva.Sum(lr => lr.CantidadReservada);
            return new ProductoDemandaDTO
            {
                ProductoId = producto.Id,
                ProductoNombre = producto.Descripcion,
                ToneladasReservadas = toneladasReservadas,
                StockDisponible = producto.StockDisponible,
                AlertaProduccion = toneladasReservadas > producto.StockDisponible
            };
        }

        public static IEnumerable<ProductoDemandaDTO> MapToProductoDemandaDTOList(IEnumerable<LineaReserva> lineasReserva, IEnumerable<Producto> productos)
        {
            return lineasReserva
                .GroupBy(lr => lr.ProductoId)
                .Select(g =>
                {
                    var producto = productos.FirstOrDefault(p => p.Id == g.Key);
                    var toneladasReservadas = g.Sum(lr => lr.CantidadReservada);
                    return new ProductoDemandaDTO
                    {
                        ProductoId = g.Key,
                        ProductoNombre = producto?.Descripcion ?? "Producto no encontrado",
                        ToneladasReservadas = toneladasReservadas,
                        StockDisponible = producto?.StockDisponible ?? 0,
                        AlertaProduccion = toneladasReservadas > (producto?.StockDisponible ?? 0)
                    };
                });
        }
    }
}
