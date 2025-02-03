using ProyectoIntegradorLogicaAplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso
{
    public interface ILogin
    {
        public UsuarioDTO Login(string email, string pass);
        string GenerarToken(string userId, string rol);
    }
}
