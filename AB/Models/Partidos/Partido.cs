using AB.Models.Equipos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AB.Models.Partidos
{
    public enum EstadoPartido
    {
        Programado,
        Jugado,
        Cancelado
    }
    public class Partido
    {
        public int Id { get; set; }
        public int EquipoLocalId { get; set; }
        public int EquipoVisitanteId { get; set; }
        public DateTime Fecha { get; set; }
        public string Cancha { get; set; } = string.Empty;
        public int? GolesLocal { get; set; }
        public int? GolesVisitante { get; set; }
        public EstadoPartido Estado { get; set; }
        public Equipo? EquipoLocal { get; set; }
        public Equipo? EquipoVisitante { get; set; }
    }
}
