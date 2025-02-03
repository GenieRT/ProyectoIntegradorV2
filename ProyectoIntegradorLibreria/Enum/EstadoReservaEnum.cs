using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLibreria.Enum
{
    public enum EstadoReservaEnum
    {
        CAMION_EN_EXPLANADA, // 0
        CAMION_CARGANDO,     // 1
        CAMION_CARGADO,      // 2
        ASISTENCIA_CONFIRMADA, // 3
        RESERVA_ANULADA,     // 4
        BOLSONES_EN_PLANCHADA, // 5
        SIN_ESTADO //6
    }
}
