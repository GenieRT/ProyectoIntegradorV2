using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProyectoIntegradorLibreria.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Rol { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Contraseña { get; set; }
       
        [Required]    

        public string EncriptedPassword { get; private set; }

        public string? ConfirmationToken { get; set; }
        public string? TemporalPassword { get; set; }



        public Usuario(Usuario usuario) { }

        public Usuario()
        {
        }

        public void Validar()
        {
            ValidarPass();
        }

        private void ValidarPass()
        {
            if (string.IsNullOrWhiteSpace(Contraseña))
            {
                throw new Exception("La contraseña es requerida.");
            }
            else if (Contraseña.Length < 8 || !Regex.IsMatch(Contraseña, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\.,;:#'!]).+$"))
            {
                throw new Exception("La contraseña no cumple con los requisitos. Debe tener al menos 8 caracteres, al menos una letra minúscula, al menos una letra mayúscula, al menos un número y al menos un carácter especial (.,;:#'!).");
            }

        }


        // ----------------------- HASH --------------------------------

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using SHA256 sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8 || !Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\.,;:#'!]).+$"))
            {
                throw new Exception("La contraseña no cumple con los requisitos...");
            }
            this.Contraseña = password; // Aunque no es recomendable guardar esto
            this.EncriptedPassword = ComputeSha256Hash(password); // Encriptar y almacenar
        }

    }








}
