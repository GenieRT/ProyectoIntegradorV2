using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLogicaAplicacion.DTOs;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProyectoIntegrador.WebApiVersion3.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private IRegistrarPedido _registrarPedido;
        private IListarPedidos listarPedidosCU;
        private IAprobarPedido aprobarPedidoCU;
        private IObtenerPedidoPorId _obtenerPedidoPorId;
        private IListarPedidosPendientes _listarPedidosPendientesCU;


        public PedidoController(IRegistrarPedido registrarPedido, IListarPedidos listarPedidos, IAprobarPedido aprobarPedido, IObtenerPedidoPorId obtenerPedidoPorId, IListarPedidosPendientes listarPedidosPendientesCU)
        {
            _registrarPedido = registrarPedido;
            listarPedidosCU = listarPedidos;
            aprobarPedidoCU = aprobarPedido;
            _obtenerPedidoPorId = obtenerPedidoPorId;
            _listarPedidosPendientesCU = listarPedidosPendientesCU;
        }


        [HttpGet("PedidosYReservas")]
        [Authorize(Roles = "Empleado,Cliente")]
        public IActionResult ObtenerPedidosYReservas(int clienteId)
        {
            try
            {
                var pedidosReservas = listarPedidosCU.ObtenerPedidosYReservasPorCliente(clienteId);
                return Ok(pedidosReservas);
            }
            catch (ArgumentException ex) // Excepción para parámetros inválidos
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex) // Excepción para recursos no encontrados
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) // Excepción general para errores internos
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("PedidosPendientes")]
        [Authorize(Roles = "Empleado")]
        public IActionResult GetPedidosPendientes()
        {
            try
            {
                var pedidosPendientes = _listarPedidosPendientesCU.listar();
                return Ok(pedidosPendientes);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }



            [HttpPut("AprobarPedido")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Empleado")]
        public IActionResult AprobarPedido([FromQuery] int pedidoId)
        {
            try
            {
                aprobarPedidoCU.AprobarPedidoPorId(pedidoId);
                return Ok("Pedido aprobado con éxito.");
            }
            catch (ArgumentException ex) // Parámetros inválidos
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex) // Pedido no encontrado
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex) // Lógica de negocio fallida
            {
                return StatusCode(403, ex.Message); // HTTP 403 para operaciones no permitidas
            }
            catch (Exception ex) // Error interno
            {
                return StatusCode(500, new { message = "Error interno del servidor", detalle = ex.Message });
            }
        }



        // GET: api/v1/pedidos
        /*[HttpGet]
        public IActionResult GetAll()
        {
            var pedidos = _repositorioPedidos.FindAll();
            if (pedidos == null || !pedidos.Any())
                return NoContent();
            return Ok(pedidos);
        }*/

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Empleado,Cliente")]
        public ActionResult Get(int id)
        {

            try
            {
                return Ok(this._obtenerPedidoPorId.Ejecutar(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/v1/pedidos
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Cliente")]
        public IActionResult Post ([FromBody] PedidoDTO pedido)
        {
            try
            {
                PedidoDTO pedDTO = this._registrarPedido.addPedido(pedido);
                return Created("api/v1/Pedido", pedDTO);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/v1/pedidos/{id}
        /*[HttpPut("{id}")]
        public IActionResult Update(int id, Pedido pedido)
        {
            if (pedido == null || pedido.Id != id)
                return BadRequest("El ID del pedido no coincide o el pedido es nulo.");

            _repositorioPedidos.Update(pedido);
            return NoContent();
        }

        // DELETE: api/v1/pedidos/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pedido = _repositorioPedidos.FindByID(id);
            if (pedido == null)
                return NotFound($"No se encontró el pedido con ID {id}");

            _repositorioPedidos.Remove(id);
            return NoContent();
        }*/

        

    }
}