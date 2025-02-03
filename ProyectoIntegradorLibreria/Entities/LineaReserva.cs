using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLibreria.Entities
{
    public class LineaReserva
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Producto))] public int ProductoId { get; set; }

        public Producto ? Producto { get; set; }
        public int  CantidadReservada { get; set; }

        public LineaReserva() { }
    }
}
