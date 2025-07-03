using clase_cinco_api_autenticacion.Modelos;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace clase_cinco_api_autenticacion.Servicio
{
    public interface IGeneradorToken
    {
        string GenerarToken(User user);
    }

    public class GeneradorToken : IGeneradorToken
    {
        public string GenerarToken(User user)
        {
            string claveSecreta = Environment.GetEnvironmentVariable("CLAVE_SECRETA_JWT") ?? string.Empty;
            string tiempoExpiracion = Environment.GetEnvironmentVariable("TIEMPO_EXPIRACION_JWT") ?? "1";

            var claveSecretaByte = Encoding.UTF8.GetBytes(claveSecreta);

            SecurityTokenDescriptor descriptor = new()
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Name, user.NombreCompleto),
                    new Claim(ClaimTypes.Email, user.Email),
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(tiempoExpiracion)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(claveSecretaByte),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new();

            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
