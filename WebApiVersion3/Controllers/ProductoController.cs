using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoIntegradorWebApi2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private IListarProductos _listarProductos;
        private IObtenerProductoPorId _productoPorId;

        public ProductoController(IListarProductos listarProductos, IObtenerProductoPorId obtenerProductoPorId)
        {
            _listarProductos = listarProductos;
            _productoPorId = obtenerProductoPorId;
        }

        // Obtener todos los productos
        [HttpGet(Name = "GetProductos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Empleado,Cliente")]
        public ActionResult Get()
        {
            try
            {

                return Ok(_listarProductos.ListarProductos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Empleado,Cliente")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(this._productoPorId.Ejecutar(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            /* private readonly IRepositorioProductos _repositorioProductos;

            // Constructor inyectando el repositorio de productos
            public ProductoController(IRepositorioProductos repositorioProductos)
            {
                _repositorioProductos = repositorioProductos;
            }



            // Obtener productos filtrados por nombre
            [HttpGet("filtro/nombre")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult GetByNombre([FromQuery] string nombre)
            {
                try
                {
                    // Validar que el nombre no esté vacío
                    if (string.IsNullOrEmpty(nombre))
                    {
                        return BadRequest("El filtro de nombre es obligatorio.");
                    }

                    var productos = _repositorioProductos.FindAll()
                        .Where(p => p.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();

                    // Verificar si se encontraron productos
                    if (productos.Count == 0)
                    {
                        return StatusCode(204, "No se encontraron productos con el nombre proporcionado.");
                    }

                    return Ok(productos);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Hubo un error al obtener los productos.");
                }
            }



            // Obtener un producto por su ID

            }*/
        }
    }
}