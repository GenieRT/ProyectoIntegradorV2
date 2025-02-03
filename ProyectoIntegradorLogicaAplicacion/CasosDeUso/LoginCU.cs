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
    public class LoginCU : ILogin
    {
        private IRepositorioUsuarios repoUsuarios;
        private ITokenService tokenService;

        public LoginCU(IRepositorioUsuarios repositorioUsuarios, ITokenService tokenService)
        {
            this.repoUsuarios = repositorioUsuarios;
            this.tokenService = tokenService;
        }

        public UsuarioDTO Login(string email, string pass)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                throw new ArgumentException("El email y la contraseña son requeridos.");
            }

            var usuario = repoUsuarios.FindByEmail(email);
            if (usuario == null)
            {
                throw new Exception("El email no está registrado.");
            }

            string claveEncriptada = Usuario.ComputeSha256Hash(pass);
            if (usuario.EncriptedPassword != claveEncriptada)
            {
                throw new Exception("Contraseña incorrecta.");
            }

            return new UsuarioDTO(usuario);
        }
        
        
        public string GenerarToken(string userId, string rol)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(rol))
            {
                throw new ArgumentException("Los datos del usuario son inválidos para generar el token.");
            }

            try
            {
                return tokenService.GenerateToken(userId, rol);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar el token: {ex.Message}", ex);
            }

        }
    }
}
