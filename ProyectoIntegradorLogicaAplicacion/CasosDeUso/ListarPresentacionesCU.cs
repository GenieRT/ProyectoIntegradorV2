using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using ProyectoIntegradorLogicaAplicacion.DTOs;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.CasosDeUso
{
    public class ListarPresentacionesCU : IListarPresentaciones
    {
        IRepositorioPresentaciones _repositorioPresentaciones;

        public ListarPresentacionesCU(IRepositorioPresentaciones repositorioPresentaciones)
        {
            _repositorioPresentaciones = repositorioPresentaciones;
        }
        public IEnumerable<PresentacionDTO> ListarPresentaciones()
        {
            List<PresentacionDTO> aRetornar = new List<PresentacionDTO>();
            foreach(Presentacion p in _repositorioPresentaciones.FindAll().ToList())
            {
                aRetornar.Add(new PresentacionDTO(p));
            }
            return aRetornar;
        }
    }
}
