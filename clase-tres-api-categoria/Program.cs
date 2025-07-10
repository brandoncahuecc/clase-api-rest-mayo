using clase_cinco_biblioteca.Dependencias;
using clase_cinco_biblioteca.Middlewares;
using clase_tres_api_categoria.Persistencia;
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
builder.Services.RegistrarReddis();
builder.Services.RegistrarValidacionTokenJwt();

builder.Services.AddSingleton<ICategoriaPersistencia, CategoriaPersistencia>();
builder.Services.AddSingleton<ICachePersistencia, CachePersistencia>();

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
