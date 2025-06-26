using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clase_cinco_biblioteca.Dependencias
{
    public static class ReddisDependencia
    {
        public static IServiceCollection RegistrarReddis(this IServiceCollection services) =>
            services.AddStackExchangeRedisCache(option =>
            {
                string redisConnection = Environment.GetEnvironmentVariable("REDIS_CONNECTION") ?? string.Empty;
                option.Configuration = redisConnection;
            });
    }
}
