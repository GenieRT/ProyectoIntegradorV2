using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Microsoft.Identity.Client;
using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using ProyectoIntegradorLogicaAplicacion.DTOs;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ProyectoIntegrador.WebApi2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TurnosCargaController : ControllerBase
    {
        private IRegistrarTurnoCarga _registrarTurnoCarga;


        public TurnosCargaController(IRegistrarTurnoCarga registrarTurnoCarga)
        {

            _registrarTurnoCarga = registrarTurnoCarga;
        }

        //Registro de Turno de Carga
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Empleado")]

        public IActionResult Post([FromBody] TurnoCargaDTO turnoCarga)
        {
            try
            {
                TurnoCargaDTO turnoCargaDTO = _registrarTurnoCarga.Ejecutar(turnoCarga);
                return Created("api/v1/TurnoCarga", turnoCargaDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }


}
