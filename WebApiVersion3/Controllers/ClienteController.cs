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
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
