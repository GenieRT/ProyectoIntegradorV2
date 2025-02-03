using ProyectoIntegradorLibreria.Entities;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorAccesData.EntityFramework.SQL
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private ISUSAContext _context;

        public RepositorioUsuarios()
        {
            _context = new ISUSAContext();
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }


        public Usuario? FindByID(int id)
        {
            return _context.Usuarios.Find(id);
        }


        public bool FindBy(string email, string pass)
        {
            var buscar = _context.Usuarios.Where(u => u.Contraseña == pass && u.Email == email).SingleOrDefault();

            if (buscar == null)
            {
                return false;
            }
            return true;
        }


        public Usuario? FindByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("El email no puede estar vacío.");
            }

            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public void Add(Usuario t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> GetAllClientes()
        {
            if (_context == null)
            {
                throw new InvalidOperationException("El contexto de la base de datos no está disponible.");
            }

            return _context.Usuarios
                           .Where(u => u.Rol == "Cliente")
                           .Cast<Cliente>()
                           .ToList();
        }
    }
}
