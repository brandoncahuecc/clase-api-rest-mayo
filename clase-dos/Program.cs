using clase_dos.Persistencia;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string cadena = Environment.GetEnvironmentVariable("CADENA") ??
    "Server=192.168.1.20;Database=Clase02;User Id=SA;Password=Ab123456*;TrustServerCertificate=True";

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(cadena));

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
