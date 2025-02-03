using Microsoft.EntityFrameworkCore;
using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorAccesData
{
    public class ISUSAContext : DbContext
    {

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Presentacion> Presentacions { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<TurnoCarga> TurnosCargas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
       .UseSqlServer(@"Server=tcp:isusa.database.windows.net,1433;Initial Catalog=ISUSA;Persist Security Info=False;User ID=m.e.tissot;Password=proyecto2024.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
           sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null))
       .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ValueConverter para EstadoReserva
            modelBuilder.Entity<Reserva>()
                .Property(r => r.EstadoReserva)
                .HasConversion(
                    v => v.ToString(), // Convierte Enum -> String al guardar en la base de datos
                    v => (EstadoReservaEnum)Enum.Parse(typeof(EstadoReservaEnum), v) // Convierte String -> Enum al leer de la base de datos
                );

            modelBuilder.Entity<Usuario>()
                        .HasDiscriminator<string>("Discriminator")
                        .HasValue<Usuario>("Usuario") // Valor por defecto para usuarios genéricos
                        .HasValue<Cliente>("Cliente") // Discriminador para Cliente
                        .HasValue<Empleado>("Empleado"); // Discriminador para Empleado

            // Configuración para Cliente
            modelBuilder.Entity<Cliente>()
                .HasBaseType<Usuario>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Cliente>("Cliente");

            // Configuración para Empleado
            modelBuilder.Entity<Empleado>()
                .HasBaseType<Usuario>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Empleado>("Empleado");

            modelBuilder.Entity<Pedido>().HasMany(p => p.Productos);

            modelBuilder.Entity<Pedido>()
             .HasOne(p => p.Cliente)
     .WithMany()
             .HasForeignKey(p => p.ClienteId)
     .OnDelete(DeleteBehavior.Restrict); // Cambia Cascade por Restrict
           
            /* modelBuilder.Entity<LineaPedido>()
       .HasOne(lp => lp.Pedido)
       .WithMany(p => p.Productos)
       .HasForeignKey(lp => lp.PedidoId)
       .OnDelete(DeleteBehavior.Cascade);

   modelBuilder.Entity<LineaPedido>()
       .HasOne(lp => lp.Producto)
       .WithMany()
       .HasForeignKey(lp => lp.ProductoId)
       .OnDelete(DeleteBehavior.Restrict);
  */

            modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Cliente)
            .WithMany()
            .HasForeignKey(r => r.ClienteId)
            .OnDelete(DeleteBehavior.Restrict); // Cambia Cascade por Restrict



            modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Pedido)
            .WithMany()
            .HasForeignKey(r => r.PedidoId)
            .OnDelete(DeleteBehavior.Restrict); // Cambia Cascade por Restrict

            /*modelBuilder.Entity<LineaPedido>()
            .HasKey(lp => new { lp.ProductoId, lp.PresentacionId, lp.PedidoId });*/
        }

    }
}
