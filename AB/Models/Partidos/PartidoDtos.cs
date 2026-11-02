using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AB.Models.Partidos
{
    public record PartidoDto
    {
        public int Id { get; set; }
        public int EquipoLocalId { get; set; }
        public int EquipoVisitanteId { get; set; }
        public DateTime Fecha { get; set; }
        public string Cancha { get; set; } = string.Empty;
        public int? GolesLocal { get; set; }
        public int? GolesVisitante { get; set; }
        public EstadoPartido Estado { get; set; }
    }

    public class PartidoCrearDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar un Equipo Local")]
        public int EquipoLocalId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar un Equipo Visitante")]
        public int EquipoVisitanteId { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "La cancha debe tener entre 2 y 100 caracteres")]
        public string Cancha { get; set; } = string.Empty;
    }

    public class PartidoActualizarDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar un equipo local válido")]
        public int EquipoLocalId { get; set; } 
        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar un equipo visitante válido")]
        public int EquipoVisitanteId { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "La cancha debe tener entre 2 y 100 caracteres")]
        public string Cancha { get; set; } = string.Empty;
        public EstadoPartido Estado { get; set; }
        [Range(0, 99, ErrorMessage = "Los goles deben estar entre 0 y 99")]
        public int? GolesLocal { get; set; }
        [Range(0, 99, ErrorMessage = "Los goles deben estar entre 0 y 99")]
        public int? GolesVisitante { get; set; }
    }
}
