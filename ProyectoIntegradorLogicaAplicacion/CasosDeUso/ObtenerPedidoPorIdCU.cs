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
    public class ObtenerPedidoPorIdCU : IObtenerPedidoPorId
    {

        private readonly IRepositorioPedidos repoPedidos;

        public ObtenerPedidoPorIdCU(IRepositorioPedidos repositorioPedidos)
        {
            this.repoPedidos = repositorioPedidos;
        }
        public PedidoDTO Ejecutar(int id)
        {
            Pedido ped = repoPedidos.GetPedidoById(id);
            if(ped == null)
            {
                throw new Exception("Pedido no encontrado");
            }
            return PedidoMapper.ToDto(ped);
        }
    }
}
