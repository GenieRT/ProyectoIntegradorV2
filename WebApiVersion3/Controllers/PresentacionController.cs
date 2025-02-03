using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;

namespace WebApiVersion3.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PresentacionController : ControllerBase
    {
        private IListarPresentaciones _listarPresentaciones;

        public PresentacionController(IListarPresentaciones listarPresentaciones) 
        { 
            _listarPresentaciones = listarPresentaciones;
        }

        [HttpGet(Name = "GetPresentaciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Empleado,Cliente")]
        public ActionResult Get()
        {
            try
            {

                return Ok(_listarPresentaciones.ListarPresentaciones());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
