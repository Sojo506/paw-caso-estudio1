using AB.Interfaces.Jugadores;
using AB.Models.Jugadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Jugadores
{
    public class JugadorBL : IJugadorBL
    {
        private const int EdadMinima = 16;
        private readonly IJugadorDA _da;

        public JugadorBL(IJugadorDA da)
        {
            _da = da;
        }

        public async Task ActualizarAsync(int id, JugadorActualizarDto dto, CancellationToken ct)
        {
            ValidarEdadMinima(dto.FechaNacimiento);

            var actualizado = await _da.ActualizarJugadorAsync(new Jugador
            {
                Id = id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaNacimiento = dto.FechaNacimiento,
                NumeroCamiseta = dto.NumeroCamiseta,
                Posicion = dto.Posicion,
                EquipoId = dto.EquipoId,
                Estado = dto.Estado,
            }, ct);

            if (!actualizado)
            {
                throw new KeyNotFoundException("No se encontró el jugador para actualizar.");
            }
        }

        public async Task<JugadorDto> CrearJugadorAsync(JugadorCrearDto dto, CancellationToken ct)
        {
            ValidarEdadMinima(dto.FechaNacimiento);

            var jugador = new Jugador
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaNacimiento = dto.FechaNacimiento,
                NumeroCamiseta = dto.NumeroCamiseta,
                Posicion = dto.Posicion,
                EquipoId = dto.EquipoId,
                Estado = dto.Estado,
            };

            await _da.CrearJugadorAsync(jugador, ct);
            return JugadorMapeador.ADto(jugador);
        }

        public async Task EliminarAsync(int id, CancellationToken ct)
        {
            var eliminado = await _da.EliminarJugadorAsync(id, ct);

            if (!eliminado)
            {
                throw new KeyNotFoundException("No se encontró el jugador para eliminar.");
            }
        }

        public async Task<List<JugadorDto>> ListarJugadoresAsync(CancellationToken ct)
        {
            var jugadores = await _da.ListarJugadoresAsync(ct);
            return jugadores.Select(JugadorMapeador.ADto).ToList();
        }

        public async Task<JugadorDto?> ObtenerJugadorPorIdAsync(int id, CancellationToken ct)
        {
            var jugador = await _da.ObtenerJugadorPorIdAsync(id, ct);
            return jugador != null ? JugadorMapeador.ADto(jugador) : null;
        }

        private static void ValidarEdadMinima(DateTime fechaNacimiento)
        {
            var hoy = DateTime.UtcNow.Date;
            var nacimiento = fechaNacimiento.Date;
            var edad = hoy.Year - nacimiento.Year;

            if (nacimiento > hoy.AddYears(-edad))
            {
                edad--;
            }

            if (edad < EdadMinima)
            {
                throw new InvalidOperationException($"El jugador debe tener al menos {EdadMinima} años.");
            }
        }
    }
}

