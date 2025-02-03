using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLibreria.Entities
{
    public class LineaPedido
    {
        
        public int Id { get; set; }
        [ForeignKey(nameof(Producto))] public int  ProductoId { get; set; }

        public Producto Producto { get; set; }
        public Presentacion Presentacion { get; set; }

       [ForeignKey (nameof(Presentacion))] public int PresentacionId { get; set; }
        public int Cantidad { get; set; }

        public int CantidadRestante { get; set; }

        public LineaPedido() 
        {
        
        }
    }

}
