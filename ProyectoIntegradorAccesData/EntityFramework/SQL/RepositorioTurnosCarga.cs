using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorAccesData.EntityFramework.SQL
{
    public class RepositorioTurnosCarga : IRepositorioTurnosCarga
    {
        private ISUSAContext _context;

        public RepositorioTurnosCarga()
        {
            _context = new ISUSAContext();
        }
        public void Add(TurnoCarga t)
        {
            if (t.Toneladas == 0)
            {
                throw new Exception("La cantidad de toneladas no puede ser cero.");
            }

            // Verificar que la fecha de inicio no sea menor a la fecha actual
            if (t.FechaInicioSemana < DateTime.Now)
            {
                throw new Exception("La fecha de inicio no puede ser menor a la fecha actual.");
            }

            // Verificar que la fecha de fin no sea menor a la fecha de inicio
            if (t.FechaFinSemana < t.FechaInicioSemana)
            {
                throw new Exception("La fecha de fin no puede ser menor a la fecha de inicio.");
            }

            // Verificar si hay un turno que se solape con las fechas ingresadas
            bool existeSuperposicion = _context.TurnosCargas
                .Any(tc => t.FechaInicioSemana < tc.FechaFinSemana && t.FechaInicioSemana > tc.FechaInicioSemana);

            if (existeSuperposicion)
            {
                throw new Exception("Ya existe un turno de carga registrado que se solapa con el rango de fechas ingresado.");
            }

            _context.TurnosCargas.Add(t);
            _context.SaveChanges();
        }


        public TurnoCarga ObtenerTurnoPorFecha(DateTime fecha)
        {
            return _context.TurnosCargas
                .FirstOrDefault(t => fecha >= t.FechaInicioSemana && fecha <= t.FechaFinSemana);
        }

    }
}
