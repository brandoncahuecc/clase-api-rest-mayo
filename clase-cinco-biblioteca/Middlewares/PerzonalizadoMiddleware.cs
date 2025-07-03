using clase_cinco_biblioteca.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace clase_cinco_biblioteca.Middlewares
{
    public class PerzonalizadoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerzonalizadoMiddleware> _logger;
        public PerzonalizadoMiddleware(RequestDelegate next, ILogger<PerzonalizadoMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"Solicitud Api: {context.Request.Method} - {context.Request.Path}");

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error general en el API");

                Mensaje mensaje = new("E-G-A", "Tuvimos problemas no controlados, contacte al administrador", ex.Message);

                var respuesta = context.Response;
                respuesta.StatusCode = 500;
                respuesta.ContentType = "application/json";
                await respuesta.WriteAsync(JsonConvert.SerializeObject(mensaje));
            }

            _logger.LogInformation($"Respuesta Api: {context.Response.StatusCode}");
        }
    }
}
