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
    public class ObtenerReservasSemanaProximaCU : IObtenerReservasProximaSemana
    {
        private IRepositorioReservas _repositorioReservas;
        private IRepositorioProductos _repositorioProductos;
        public ObtenerReservasSemanaProximaCU(IRepositorioReservas repositorioReservas, IRepositorioProductos repositorioProductos)
        {
            _repositorioReservas = repositorioReservas; 
            _repositorioProductos = repositorioProductos;
        }
        public IEnumerable<ProductoDemandaDTO> Ejecutar()
        {
            var reservas = _repositorioReservas.GetReservasProximaSemana();
            var productos = _repositorioProductos.FindAll();

            return LineaReservaMapper.MapToProductoDemandaDTOList(reservas, productos);
        }
    }
}
