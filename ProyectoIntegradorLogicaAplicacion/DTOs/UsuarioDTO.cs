using ProyectoIntegradorLibreria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }

        public UsuarioDTO() { }

        public UsuarioDTO(Usuario user)
        {
            if (user != null)
            {
                this.Id = user.Id;
                this.Nombre = user.Nombre;
                this.Rol = user.Rol;
                this.Email = user.Email;
                this.Contraseña = user.Contraseña;
            }
        }

    }
}
