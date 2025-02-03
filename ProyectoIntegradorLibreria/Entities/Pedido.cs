using ProyectoIntegradorLibreria.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLibreria.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public EstadoPedidoEnum Estado { get; set; }
        public List<LineaPedido> Productos { get; set; }
        public Cliente Cliente { get; set; }

        [ForeignKey (nameof (Cliente))] public int ClienteId { get; set; }

        public List<Reserva> Reservas { get; set; } 

        public Pedido() 
        { 
            Productos = new List<LineaPedido>();
            Reservas = new List<Reserva>();
        }

        public int GetCantidadReservada(int productoId)
        {
            return Reservas
                .SelectMany(r => r.LineasReservas)
                .Where(lr => lr.ProductoId == productoId)
                .Sum(lr => lr.CantidadReservada);
        }

        public bool EstaCompletamenteReservado()
        {
            return Productos.All(lp => GetCantidadReservada(lp.ProductoId) == lp.Cantidad);
        }

        public void SetCantidadRestante(int idProducto, int cant)
        {
            foreach (var item in Productos)
            {
                if(item.Id == idProducto)
                {
                    item.CantidadRestante = cant;
                }
            }
        }

        public void AddReservas(Reserva reserva)
        {
            Reservas.Add(reserva);
        }

        public void CerrarPedido()
        {
            this.Estado = EstadoPedidoEnum.CERRADO;
        }
        public void Validar()
        {
            ValidarLista();
        }
        private void ValidarLista()
        {
            if (Productos == null || Productos.Count == 0)
            {
                throw new Exception("El pedido debe contener al menos un producto.");
            }

            foreach (var item in Productos)
            {
                if (item.Cantidad == 0)
                {
                    throw new Exception("La cantidad no puede ser 0.");
                }
            }

            for (int i = 0; i < Productos.Count; i++)
            {
                for (int j = i + 1; j < Productos.Count; j++)
                {
                    if (Productos[i].ProductoId == Productos[j].ProductoId)
                    {
                        throw new Exception($"El producto con ID {Productos[i].ProductoId} está repetido en el pedido.");
                    }
                }
            }
        }

    }
}
