using ProyectoIntegradorLogicaAplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso
{
    public interface IListarReservas
    {
        public IEnumerable<ReservaDTO> GetReservasPorCliente(int clienteId);
        public IEnumerable<ReservaDTO> GetReservasEmplados();

    }
}
