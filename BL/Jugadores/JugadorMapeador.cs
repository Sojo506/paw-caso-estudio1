using AB.Models.Jugadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Jugadores
{
    internal static class JugadorMapeador
    {
        public static JugadorDto ADto(Jugador jugador) =>
            new JugadorDto
            {
                Id = jugador.Id,
                Nombre = jugador.Nombre,
                Apellido = jugador.Apellido,
                FechaNacimiento = jugador.FechaNacimiento,
                NumeroCamiseta = jugador.NumeroCamiseta,
                Posicion = jugador.Posicion,
                EquipoId = jugador.EquipoId,
                Estado = jugador.Estado
            };
    }
}

