using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLibreria.Entities
{
    public class Empleado : Usuario
    {
        public int? NumeroEmpleado { get; set; }

        public Empleado() { }

    }
}
