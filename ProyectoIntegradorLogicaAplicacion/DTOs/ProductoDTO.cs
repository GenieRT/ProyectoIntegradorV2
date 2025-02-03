using ProyectoIntegradorLibreria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public float Costo { get; set; }
        public int StockDisponible { get; set; }

        public ProductoDTO() { }

        public ProductoDTO (Producto prodcuto)
        {
            Id = prodcuto.Id;
            Descripcion = prodcuto.Descripcion;
            Costo = prodcuto.Costo;
            StockDisponible = prodcuto.StockDisponible;
        }
    }
}
