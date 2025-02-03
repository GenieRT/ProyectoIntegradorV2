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
    public class ListarReservasCU : IListarReservas
    {

        IRepositorioReservas _repositorioReservas;

        public ListarReservasCU(IRepositorioReservas repositorioReservas)
        {
            this._repositorioReservas = repositorioReservas;
        }



        public IEnumerable<ReservaDTO> GetReservasEmplados()
        {
            List<Reserva> reservas = _repositorioReservas.FindAll().ToList();
            return ReservaMapper.ToListaDto(reservas);
        }

        public IEnumerable<ReservaDTO> GetReservasPorCliente(int clienteId)
        {
            throw new NotImplementedException();
        }
    }
}
