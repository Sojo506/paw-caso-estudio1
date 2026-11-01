using AB.Interfaces.Equipos;
using BL.Equipos;
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
            return servicios;
        }
    }
}
