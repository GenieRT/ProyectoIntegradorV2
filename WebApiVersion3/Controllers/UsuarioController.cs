using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegradorLogicaAplicacion.DTOs;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using WebApiVersion3.Services;

namespace WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IRegistro RegistroCU;
        private ILogin LoginCU;
        private readonly IEmailService emailService;
        private readonly ITokenService tokenService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UsuarioController(IRegistro registroCU, ILogin loginCU, IEmailService emailService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            RegistroCU = registroCU;
            LoginCU = loginCU;
            this.emailService = emailService;
            this.tokenService = tokenService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPut("ActualizarContraseña")]
        public ActionResult ActualizarContraseña([FromBody] ActualizarContrasenaDTO datos)
        {
            try
            {
                RegistroCU.ActualizarContraseña(datos.Email, datos.NuevaContraseña, httpContextAccessor.HttpContext);
                return Ok(new { mensaje = "Proceso exitoso. Por favor, revisa tu correo para confirmar tu contraseña." });
            }
            catch (ArgumentException ex) // Validaciones del caso de uso
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex) // Errores de lógica del negocio
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex) // Otros errores inesperados
            {
                return StatusCode(500, new { message = "Error interno del servidor", detalle = ex.Message });
            }

        }




        [HttpGet("ConfirmarCambio")]
        public ActionResult ConfirmarCambio(string email, string token)
        {
            try
            {
                RegistroCU.ConfirmarCambio(email, token);
                return Ok("El cambio de contraseña ha sido confirmado exitosamente.");
            }
            catch (ArgumentException ex) // Validaciones del caso de uso
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex) // Errores de lógica del negocio
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex) // Otros errores inesperados
            {
                return StatusCode(500, new { message = "Error interno del servidor", detalle = ex.Message });
            }

        }




        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("IniciarSesion")]
        public ActionResult Login([FromBody] Dictionary<string, string> loginData)
        {
            try
            {
                string email = loginData["email"];
                string pass = loginData["pass"];

                UsuarioDTO usuario = LoginCU.Login(email, pass);
                string token = LoginCU.GenerarToken(usuario.Id.ToString(), usuario.Rol);

                return Ok(new { id = usuario.Id, usuario, token, role = usuario.Rol });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", detalle = ex.Message });
            }
        }




    }
}
