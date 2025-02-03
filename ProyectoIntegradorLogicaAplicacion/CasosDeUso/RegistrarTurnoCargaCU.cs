using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using ProyectoIntegradorLogicaAplicacion.DTOs.Mapper;
using ProyectoIntegradorLogicaAplicacion.DTOs;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.CasosDeUso
{

        public class RegistraTurnoCargaCU : IRegistrarTurnoCarga
        {
            public IRepositorioTurnosCarga _repositorioTurnosCarga;

            public RegistraTurnoCargaCU(IRepositorioTurnosCarga repositorioTurnosCarga)
            {
            _repositorioTurnosCarga = repositorioTurnosCarga;
            }

            public TurnoCargaDTO Ejecutar(TurnoCargaDTO turnoCarga)
            {
                if (turnoCarga == null)
                {
                    throw new ArgumentNullException(nameof(turnoCarga), "El turno de carga no puede ser nulo");
                }
                TurnoCarga nuevo = TurnosCargaMapper.FromDto(turnoCarga);
            _repositorioTurnosCarga.Add(nuevo);
                return TurnosCargaMapper.ToDto(nuevo);
            }
        }
    }

