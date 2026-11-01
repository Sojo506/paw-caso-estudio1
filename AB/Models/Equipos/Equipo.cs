using System;
using System.Collections.Generic;
using System.Text;

namespace AB.Models.Equipos
{
    public enum EstadoEquipo
    {
        Activo,
        Inactivo
    }

    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public DateTime FechaFundacion { get; set; }
        public EstadoEquipo Estado { get; set; }

        // 1 - N un equipo tiene muchos jugadores
        // public List<Jugador> Jugadores { get; set; } = new();

    }
}
