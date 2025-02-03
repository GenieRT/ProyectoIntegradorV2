using ProyectoIntegradorLibreria.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs
{
    public class LineaReservaDTO
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }

        public ProductoDTO? Producto { get; set; }
        public int CantidadReservada { get; set; }

        public LineaReservaDTO() { }

        public LineaReservaDTO(LineaReserva lineaReserva)
        {
            this.Id = lineaReserva.Id;
            this.ProductoId = lineaReserva.ProductoId;
           this.Producto = new ProductoDTO(lineaReserva.Producto);
            this.CantidadReservada = lineaReserva.CantidadReservada;
        }
    }
}
