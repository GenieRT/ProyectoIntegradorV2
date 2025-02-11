using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using ProyectoIntegradorLogicaAplicacion.DTOs;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;

namespace ProyectoIntegrador.WebApi2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private IRegistrarReserva _registrarReserva;
        private IObtenerReservasProximaSemana _listarReservasProximaSemana;
        //private IVerificarDemandaYProduccion _verificarDemandaProduccion;

        public ReservaController(IRegistrarReserva registrarReserva, IObtenerReservasProximaSemana reservasProximaSemana) {
            _registrarReserva = registrarReserva;
            _listarReservasProximaSemana = reservasProximaSemana;
           // _verificarDemandaProduccion = verificarDemandaYProduccion;
        }

        //Registro de Reserva
        [HttpPost]
        [Authorize(Roles = "Cliente")]
        public IActionResult Post([FromBody]ReservaDTO reserva)
        {
            try
            {
                ReservaDTO reservaDTO = _registrarReserva.Ejecutar(reserva);
                return Created("api/v1/Reserva", reservaDTO);
            }
            catch (ArgumentNullException ex)
            {
                // Capturar excepciones para argumentos nulos
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Capturar excepciones de operación inválida
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Capturar cualquier otra excepción como error genérico
                return StatusCode(500, new { message = "Error interno del servidor", detalle = ex.Message });
            }
        }



        // Obtener productos reservados para la próxima semana
        [HttpGet("ReservasSemanaProxima")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Empleado")]
        public IActionResult GetReservasSemanaProxima()
        {
            try
            {
                // Llama al caso de uso correspondiente para obtener los productos reservados
                var productosReservados = _listarReservasProximaSemana.Ejecutar();

                if (productosReservados == null || !productosReservados.Any())
                {
                    return NotFound("No se encontraron productos reservados para la semana próxima.");
                }

                return Ok(productosReservados);
            }
            catch (Exception ex)
            {
                // Manejo de errores general
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // Verificar demanda de producción
       /* [HttpGet("VerificarDemandaProduccion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Empleado")]
        public IActionResult VerificarDemandaProduccion()
        {
            try
            {
                var productosConAlerta = _verificarDemandaProduccion.Ejecutar(true);
                if (!productosConAlerta.Any())
                {
                    return NotFound("No se encontraron productos con alertas de producción.");
                }

                return Ok(productosConAlerta);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
       */

        
    }
}