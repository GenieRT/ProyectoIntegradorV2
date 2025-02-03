using Microsoft.IdentityModel.Tokens;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApiVersion3.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(string secretKey, string issuer, string audience)
        {
            _secretKey = secretKey;
            _issuer = issuer;
            _audience = audience;
        }

        public string GenerateToken(string userId, string role)
        {
            Console.WriteLine($"Generando token para UserId: {userId}, Role: {role}");
         

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            Console.WriteLine($"Claims generados: {string.Join(", ", claims.Select(c => $"{c.Type}: {c.Value}"))}");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            Console.WriteLine($"Clave secreta utilizada: {_secretKey}");

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // El token será válido por 2 horas
                signingCredentials: creds
            );

            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine($"Token generado: {generatedToken}");


            return generatedToken;
        }
    }
}
