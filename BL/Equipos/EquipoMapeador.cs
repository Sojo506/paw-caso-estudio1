using AB.Models.Equipos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Equipos
{
    internal static class EquipoMapeador
    {
        public static EquipoDto ADto(Equipo equipo) =>
            new EquipoDto
            {
                Id = equipo.Id,
                Nombre = equipo.Nombre,
                Ciudad = equipo.Ciudad,
                FechaFundacion = equipo.FechaFundacion,
                Estado = equipo.Estado
            };
        }
 }

