using ProyectoIntegradorLibreria.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs
{
    public class LineaPedidoDTO
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }

        public ProductoDTO? Producto { get; set; }
        public PresentacionDTO? Presentacion { get; set; }

        public int PresentacionId { get; set; }
        public int Cantidad { get; set; }

        public int CantidadRestante { get; set; }
        public LineaPedidoDTO(LineaPedido linea)
        {
            this.ProductoId = linea.ProductoId;
            this.Producto = new ProductoDTO(linea.Producto);
            this.PresentacionId = linea.PresentacionId;
            this.Presentacion = new PresentacionDTO(linea.Presentacion); 
            this.Cantidad = linea.Cantidad;
            this.CantidadRestante = Cantidad;
           
        }


        public LineaPedidoDTO() { }
    }
}   
