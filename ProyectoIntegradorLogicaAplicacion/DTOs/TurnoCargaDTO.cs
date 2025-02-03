using ProyectoIntegradorLibreria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace ProyectoIntegradorLogicaAplicacion.DTOs
{
   public class TurnoCargaDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicioSemana { get; set; }
        public DateTime FechaFinSemana { get; set; }

        public int Toneladas { get; set; }

        public int ToneladasAcumuladas { get; set; }

        public TurnoCargaDTO() { }


        public TurnoCargaDTO(TurnoCarga turnoCarga)
        {
            try
            {
              

                if (turnoCarga == null)
                {
                    throw new ArgumentNullException(nameof(turnoCarga), "El turno no puede ser nulo.");
                }

                FechaInicioSemana = turnoCarga.FechaInicioSemana;
                FechaFinSemana = turnoCarga.FechaFinSemana;
                Toneladas = turnoCarga.Toneladas;
                ToneladasAcumuladas = 0;
            }
            catch (Exception ex)
            {
              
                throw;
            }
        }
    }


}
