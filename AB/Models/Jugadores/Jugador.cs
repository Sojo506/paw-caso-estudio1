using AB.Models.Equipos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AB.Models.Jugadores
{
    public enum EstadoJugador
    {
        Activo,
        Inactivo,
        Lesionado,
        Suspendido
    }

    public enum PosicionJugador
    {
        Portero,
        Defensa,
        Mediocampista,
        Delantero
    }

    public class Jugador
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public int NumeroCamiseta { get; set; }
        public PosicionJugador Posicion { get; set; }
        public int EquipoId { get; set; }
        public Equipo? Equipo { get; set; }
        public EstadoJugador Estado { get; set; }
    }
}

