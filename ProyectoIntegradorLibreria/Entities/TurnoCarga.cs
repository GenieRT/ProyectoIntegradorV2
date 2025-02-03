using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLibreria.Entities
{
    public class TurnoCarga
    {
        public int Id { get; set; }
        public DateTime FechaInicioSemana { get; set; }
        public DateTime FechaFinSemana { get; set; }
        public int Toneladas { get; set; }
        public int ToneladasAcumuladas { get; set; }
        public TurnoCarga() { }

        public void ActualizarToneladasAcumuladas(int toneladas)
        {

            ToneladasAcumuladas += toneladas;
        }
    }
}
