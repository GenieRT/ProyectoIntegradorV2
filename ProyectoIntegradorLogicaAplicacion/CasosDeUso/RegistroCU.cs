using Microsoft.AspNetCore.Http;
using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using ProyectoIntegradorLogicaAplicacion.DTOs;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.CasosDeUso
{
    public class RegistroCU : IRegistro
    {
        private IRepositorioUsuarios repoUsuarios;
        private readonly IEmailService emailService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RegistroCU(IRepositorioUsuarios repositorioUsuarios, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            this.repoUsuarios = repositorioUsuarios;
            this.emailService = emailService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public UsuarioDTO BuscarUsuarioPorEmail(string email)
        {
            var usuario = repoUsuarios.FindByEmail(email);
            if (usuario == null)
            {
                return null;
            }

            return new UsuarioDTO(usuario);
        }

        //para buscar directamente la entidad Usuario
        public Usuario BuscarUsuarioEntidadPorEmail(string email)
        {
            var usuario = repoUsuarios.FindByEmail(email);
            if (usuario == null)
            {
                return null;
            }

            return usuario;
        }




        public void ActualizarContraseña(string email, string nuevaContraseña, HttpContext httpContext)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(nuevaContraseña))
            {
                throw new ArgumentException("El email y la nueva contraseña son requeridos.");
            }

            var usuario = repoUsuarios.FindByEmail(email);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            // Validar la nueva contraseña
            usuario.Contraseña = nuevaContraseña; // Asignación temporal
            usuario.Validar(); // Método de la entidad Usuario

            // Generar un token de confirmación
            string token = Guid.NewGuid().ToString();
            usuario.ConfirmationToken = token;
            usuario.TemporalPassword = nuevaContraseña;

            // Guardar el token y la contraseña temporal en la base de datos
            repoUsuarios.Update(usuario);

            // Construir el link de confirmación
            //string confirmationLink = $"https://localhost:7218/api/Usuario/ConfirmarCambio?email={email}&token={token}";

            // Obtener la URL base dinámicamente
            string baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
            string confirmationLink = $"{baseUrl}/api/Usuario/ConfirmarCambio?email={email}&token={token}";

            // Enviar el correo con el link de confirmación
            string subject = "Confirmación de cambio de contraseña";
            string body = $"Hola {usuario.Nombre},<br><br>" +
                          $"Hemos recibido tu solicitud para cambiar tu contraseña. Haz clic en el siguiente link para confirmar el cambio:<br>" +
                          $"<a href=\"{confirmationLink}\">Confirmar cambio de contraseña</a><br><br>" +
                          $"Si no realizaste esta solicitud, ignora este correo.";

            emailService.SendEmail(email, subject, body);
        }


        public void ConfirmarCambio(string email, string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("El email y el token son requeridos.");
            }

            var usuario = repoUsuarios.FindByEmail(email);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            //validar el token
            if (usuario.ConfirmationToken != token)
            {
                throw new Exception("Token inválido o expirado.");
            }

            //confirmar el cambio de contraseña
            usuario.SetPassword(usuario.TemporalPassword);
            usuario.TemporalPassword = null; // Limpiar la contraseña temporal
            usuario.ConfirmationToken = null; // Limpiar el token

            //actualizar usu en la bd
            repoUsuarios.Update(usuario);
        }
    }
}
