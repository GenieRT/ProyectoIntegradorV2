using ProyectoIntegradorLibreria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs.Mapper
{
    public class ProductoMapper
    {
        public static Producto FromDto(ProductoDTO producto)
        {
            return new  Producto
            {
                Descripcion = producto.Descripcion,
                Costo = producto.Costo,
                StockDisponible = producto.StockDisponible,
                
            };
        }


        public static ProductoDTO ToDto(Producto producto)
        {
            return new ProductoDTO()
            {
                Id = producto.Id,
                Descripcion = producto.Descripcion,
                Costo = producto.Costo,
                StockDisponible= producto.StockDisponible,

            };
        }

        public static IEnumerable<ProductoDTO> ToListaDto(IEnumerable<Producto> productos)
        {
            List<ProductoDTO> aux = new List<ProductoDTO>();
            foreach (var pro in productos)
            {
                ProductoDTO productoDTO = ToDto(pro);
                aux.Add(productoDTO);
            }
            return aux;
        }
    }
}

