using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorLogicaAplicacion.DTOs
{
    public class ReservaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public EstadoReservaEnum? EstadoReserva { get; set; }

        public PedidoDTO? Pedido { get; set; }

        public int PedidoId { get; set; }
        public ClienteDTO? Cliente { get; set; }
        public int ClienteId { get; set; }
        public string Camion { get; set; }
        public string Chofer { get; set; }

        public List<LineaReservaDTO> LineasReservas { get; set; }

        public ReservaDTO()
        {
            this.EstadoReserva = EstadoReservaEnum.SIN_ESTADO;
        }

        public ReservaDTO(Reserva reserva)
        {
            Console.WriteLine(reserva.ClienteId + "-" +  reserva.Id); //aca salta el error de overflow que te mande antes

            try
            {
                // Depuración: Validar datos de entrada
                if (reserva == null)
                {
                    throw new ArgumentNullException(nameof(reserva), "La reserva no puede ser nula.");
                }

                Id = reserva.Id;
                Fecha = reserva.Fecha;
                EstadoReserva = reserva.EstadoReserva;
                Pedido = new PedidoDTO(reserva.Pedido);
                PedidoId = reserva.PedidoId;
                Cliente = new ClienteDTO(reserva.Cliente);
                ClienteId = reserva.ClienteId;
                Camion = reserva.Camion;
                Chofer = reserva.Chofer;
                if (reserva?.LineasReservas != null)
                {
                    this.LineasReservas = reserva.LineasReservas.Select(lp => new LineaReservaDTO(lp)).ToList();
                }


                // Depuración: Confirmar transformación
                Console.WriteLine($"ReservaDTO creada con Id: {Id}, ClienteId: {ClienteId}, PedidoId: {PedidoId}");
            }
            catch (Exception ex)
            {
                // Depuración: Log de errores en DTO
                Console.WriteLine($"Error al crear ReservaDTO: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}

