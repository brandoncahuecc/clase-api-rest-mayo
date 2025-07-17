using clase_cinco_biblioteca.Dependencias;
using clase_cinco_biblioteca.Middlewares;
using clase_ocho_reportes.Servicios;
using DinkToPdf;
using DinkToPdf.Contracts;
using Prometheus;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.UseHttpClientMetrics();

builder.Services.RegistrarMediador<Program>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Logging.RegistrarBitacora();
builder.Services.RegistrarValidacionTokenJwt();

builder.Services.AddHttpClient("CategoriasCliente", cliente =>
{
    string uri = Environment.GetEnvironmentVariable("URI_CATEGORIAS") ?? string.Empty;

    cliente.BaseAddress = new Uri(uri);
    cliente.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("ImagenesCliente", cliente =>
{
    string uri = Environment.GetEnvironmentVariable("URI_LOGOS") ?? string.Empty;

    cliente.BaseAddress = new Uri(uri);
    cliente.DefaultRequestHeaders.Add("Accept", "application/json");
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddSingleton<IWebApiServicio, WebApiServicio>();

var app = builder.Build();

app.UseMetricServer();
app.UseHttpMetrics();

app.UseMiddleware<PerzonalizadoMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
