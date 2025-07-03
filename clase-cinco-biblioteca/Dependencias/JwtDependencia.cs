using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clase_cinco_biblioteca.Dependencias
{
    public static class JwtDependencia
    {
        public static IServiceCollection RegistrarValidacionTokenJwt(this IServiceCollection services)
        {
            string claveSecreta = Environment.GetEnvironmentVariable("CLAVE_SECRETA_JWT") ?? string.Empty;
            var claveSecretaByte = Encoding.UTF8.GetBytes(claveSecreta);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(claveSecretaByte)
                    };
                });

            return services;
        }
    }
}
