using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLibreria.Entities
{
    public class Cliente : Usuario
    {
        public int? NumeroCliente { get; set; }
        public string? RazonSocial { get; set; }
        public string? Estado { get; set; }
        public List<Pedido> Pedidos { get; set; } 

        public Cliente() 
        { 
            Pedidos = new List<Pedido>();
        }
    }
}
