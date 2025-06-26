using clase_cinco_api_autenticacion.Persistencia;
using clase_cinco_api_autenticacion.Servicio;
using clase_cinco_biblioteca.Dependencias;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegistrarMediador<Program>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Logging.RegistrarBitacora();

builder.Services.AddSingleton<IUsuarioPersistencia, UsuarioPersistencia>();
builder.Services.AddSingleton<IGeneradorToken, GeneradorToken>();

var app = builder.Build();

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
