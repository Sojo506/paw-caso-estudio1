using AB.Interfaces.Equipos;
using AB.Interfaces.Jugadores;
using AB.Interfaces.Partidos;
using DA.Equipos;
using DA.Jugadores;
using DA.Partidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA
{
    public static  class ConfigurationDA
    {
        public static IServiceCollection addData(this IServiceCollection servicios, string cadenaConexion)
        {
            servicios.AddDbContext<LigaDeportivaContexto>(opciones =>
                opciones.UseSqlServer(cadenaConexion));

            servicios.AddScoped<IEquipoDA, EquipoDA>();
            servicios.AddScoped<IJugadorDA, JugadorDA>();
            servicios.AddScoped<IPartidoDA, PartidoDA>();
            return servicios;

        }
    }
}
