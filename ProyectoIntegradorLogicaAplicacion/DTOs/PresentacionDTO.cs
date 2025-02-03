using ProyectoIntegradorLibreria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs
{
    public class PresentacionDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }  

        public PresentacionDTO() { }
        public PresentacionDTO(Presentacion presentacion) 
        { 
            this.Id = presentacion.Id;
            this.Descripcion = presentacion.Descripcion;
            this.Unidad = presentacion.Unidad;
        }
    }
}
