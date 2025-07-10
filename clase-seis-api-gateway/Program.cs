using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string ocelotEnv = Environment.GetEnvironmentVariable("OCELOT_ENVIRONMENT") ?? string.Empty;

builder.Configuration.AddJsonFile($"ocelot{ocelotEnv}.json", true, true);
builder.Services.AddOcelot();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

await app.UseOcelot();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
