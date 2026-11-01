using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AB.Models.Equipos
{
    public class EquipoCrearDto
    {
        [Required]
        [StringLength(50, ErrorMessage ="El nombre debe tener entre 2 y 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        [StringLength(100, ErrorMessage ="La ciudad debe tener entre 2 y 100 caracteres")]
        public string Ciudad { get; set; } = string.Empty;
        public EstadoEquipo Estado { get; set; }
    }

    public class EquipoActualizarDto
    {
        [Required]
        [StringLength(50, ErrorMessage ="El nombre debe tener entre 2 y 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        [StringLength(100, ErrorMessage = "La ciudad debe tener entre 2 y 100 caracteres")]
        public string Ciudad { get; set; } = string.Empty;
        public DateTime FechaFundacion { get; set; }
        public EstadoEquipo Estado { get; set; }
    }

    public record EquipoDto 
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public DateTime FechaFundacion { get; set; }
        public EstadoEquipo Estado { get; set; }

    } 
}
