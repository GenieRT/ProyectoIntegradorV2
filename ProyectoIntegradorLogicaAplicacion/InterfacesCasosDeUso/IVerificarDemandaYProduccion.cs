using ProyectoIntegradorLogicaAplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso
{
    public interface IVerificarDemandaYProduccion
    {
        public IEnumerable<ProductoDemandaDTO> Ejecutar(bool soloConAlerta);
    }
}
