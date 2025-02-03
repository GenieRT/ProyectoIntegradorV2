using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs
{
    public class ProductoDemandaDTO
    {
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public int ToneladasReservadas { get; set; }

        public decimal StockDisponible { get; set; }
        public bool AlertaProduccion { get; set; }
    }
}
