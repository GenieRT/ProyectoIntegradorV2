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
    public class RegistrarPedidoCU : IRegistrarPedido
    {
        public IRepositorioPedidos _repositorioPedidos;
       
        public RegistrarPedidoCU(IRepositorioPedidos repositorioPedidos)
        {
            _repositorioPedidos = repositorioPedidos;
        }

        public PedidoDTO addPedido(PedidoDTO pedido)
        {

            if (pedido == null)
            {
                throw new ArgumentNullException(nameof(pedido), "El pedido no puede ser nulo.");
            }


            Pedido nuevo = PedidoMapper.FromDto(pedido);
            _repositorioPedidos.Add(nuevo);

            return PedidoMapper.ToDto(nuevo);
        }
    }
}

