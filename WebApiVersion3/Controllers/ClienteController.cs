using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegradorLogicaAplicacion.CasosDeUso;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;

namespace WebApi2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private IListarClientes _listarClientesCU;


        public ClienteController(IListarClientes listarClientes)
        {
            _listarClientesCU = listarClientes;
        }

        [HttpGet]
        [Authorize(Roles = "Empleado")]
        public IActionResult GetAllClientes()
        {
            try
            {
                var clientes = _listarClientesCU.Listar();
                return Ok(clientes);
            }
            catch (InvalidOperationException ex)
            {
                // Devuelve un 400 Bad Request si el repositorio no está disponible
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex) when (ex.Message.Contains("No hay clientes registrados"))
            {
                // Devuelve un 404 Not Found si no hay clientes en el sistema
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Devuelve un 500 Internal Server Error para cualquier otro error no controlado
                return StatusCode(500, new { message = "Error interno del servidor", detalle = ex.Message });
            }
        }
    }
}
