using AB.Interfaces.Equipos;
using DA.Equipos;
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
            return servicios;

        }
    }
}
