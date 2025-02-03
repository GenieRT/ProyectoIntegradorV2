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
    public class ListarProductosCU : IListarProductos
    {
        public IRepositorioProductos _repositorioProductos;

        public ListarProductosCU(IRepositorioProductos repositorioProductos)
        {
            _repositorioProductos = repositorioProductos;
        }

        public IEnumerable<ProductoDTO> ListarProductos()
        {
            List<Producto> productos = _repositorioProductos.FindAll().ToList();
            
            return ProductoMapper.ToListaDto(productos);
        }
    }
}
