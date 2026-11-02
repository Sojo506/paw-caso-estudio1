using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AB.Models.Jugadores
{
    public class JugadorCrearDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 50 caracteres")]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Range(1, 99, ErrorMessage = "El número de camiseta debe estar entre 1 y 99")]
        public int NumeroCamiseta { get; set; }

        public PosicionJugador Posicion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar un EquipoId válido")]
        public int EquipoId { get; set; }

        public EstadoJugador Estado { get; set; }
    }

    public class JugadorActualizarDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 50 caracteres")]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Range(1, 99, ErrorMessage = "El número de camiseta debe estar entre 1 y 99")]
        public int NumeroCamiseta { get; set; }

        public PosicionJugador Posicion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar un EquipoId válido")]
        public int EquipoId { get; set; }

        public EstadoJugador Estado { get; set; }
    }

    public record JugadorDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public int NumeroCamiseta { get; set; }
        public PosicionJugador Posicion { get; set; }
        public int EquipoId { get; set; }
        public EstadoJugador Estado { get; set; }
    }
}

