using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using ProyectoIntegradorLogicaAplicacion.DTOs;
using ProyectoIntegradorLogicaAplicacion.DTOs.Mapper;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.CasosDeUso
{
    public class ObtenerProductoPorIdCU : IObtenerProductoPorId
    {

        IRepositorioProductos _repoProductos;

        public ObtenerProductoPorIdCU (IRepositorioProductos repoProductos)
        {
            _repoProductos = repoProductos;
        }
        public ProductoDTO Ejecutar(int id)
        {
            Producto prod = _repoProductos.FindByID(id);
            if(prod == null)
            {
                throw new Exception("Producto no encontrado");
            }
            return ProductoMapper.ToDto(prod);
        }
    }
}
