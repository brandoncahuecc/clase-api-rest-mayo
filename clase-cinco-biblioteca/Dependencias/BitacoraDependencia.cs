using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace clase_cinco_biblioteca.Dependencias
{
    public static class BitacoraDependencia
    {
        public static ILoggingBuilder RegistrarBitacora(this ILoggingBuilder logging)
        {
            var loggerConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("./Recursos/serilog-config.json").Build())
                .Enrich.FromLogContext().CreateLogger();

            logging.ClearProviders();
            logging.AddSerilog(loggerConfig);

            return logging;
        }
    }
}
