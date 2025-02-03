using Microsoft.AspNetCore.Http;
using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLogicaAplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso
{
    public interface IRegistro
    {
        UsuarioDTO BuscarUsuarioPorEmail(string email);
        void ActualizarContraseña(string email, string nuevaContraseña, HttpContext httpContext);
        //void ActualizarUsuario(Usuario usuario);
        Usuario BuscarUsuarioEntidadPorEmail(string email);
        void ConfirmarCambio(string email, string token);
    }

}
