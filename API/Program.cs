using AB.Interfaces.Equipos;
using BL.Equipos;
using DA;
using DA.Equipos;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using BL;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    // Los enums viajan como texto ("EnProceso") en el JSON de los controladores.
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


var cadenaConexion = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("No se encontró la cadena 'DefaultConnection'.");


builder.Services.addData(cadenaConexion);
builder.Services.addBl();

// Add services to the container.
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
app.MapControllers();

app.Run();