using AB.Interfaces.Equipos;
using AB.Interfaces.Jugadores;
using AB.Interfaces.Partidos;
using BL.Equipos;
using BL.Jugadores;
using BL.Partidos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public static class ConfigurationBL
    {
        public static IServiceCollection addBl(this IServiceCollection servicios) 
        {
            servicios.AddScoped<IEquipoBL, EquipoBL>();
            servicios.AddScoped<IJugadorBL, JugadorBL>();
            servicios.AddScoped<IPartidoBL, PartidoBL>();
            return servicios;
        }
    }
}
