using ProyectoIntegradorLogicaAplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso
{
    public interface  IRegistrarTurnoCarga
    {
        public TurnoCargaDTO Ejecutar(TurnoCargaDTO turnoCarga);
    }
}
