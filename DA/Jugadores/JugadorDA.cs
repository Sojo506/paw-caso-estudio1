using AB.Interfaces.Jugadores;
using AB.Models.Jugadores;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Jugadores
{
    public class JugadorDA : IJugadorDA
    {
        private readonly LigaDeportivaContexto _contexto;

        public JugadorDA(LigaDeportivaContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ActualizarJugadorAsync(Jugador jugador, CancellationToken ct)
        {
            var jugadorExistente = await _contexto.Jugadores.FirstOrDefaultAsync(j => j.Id == jugador.Id, ct);

            if (jugadorExistente == null)
            {
                return false;
            }

            try
            {
                jugadorExistente.Nombre = jugador.Nombre;
                jugadorExistente.Apellido = jugador.Apellido;
                jugadorExistente.FechaNacimiento = jugador.FechaNacimiento;
                jugadorExistente.NumeroCamiseta = jugador.NumeroCamiseta;
                jugadorExistente.Posicion = jugador.Posicion;
                jugadorExistente.EquipoId = jugador.EquipoId;
                jugadorExistente.Estado = jugador.Estado;

                await _contexto.SaveChangesAsync(ct);
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidOperationException("El jugador fue modificado o eliminado por otro proceso.", ex);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                switch (sqlEx.Number)
                {
                    case 2601:
                    case 2627:
                        throw new InvalidOperationException("El número de camiseta ya está asignado a otro jugador en este equipo.", ex);

                    case 547:
                        throw new InvalidOperationException("El equipo especificado no existe o viola una restricción de integridad.", ex);

                    case 515:
                        throw new InvalidOperationException("Uno o más campos obligatorios no pueden quedar vacíos.", ex);

                    case 2628:
                    case 8152:
                        throw new InvalidOperationException("Uno de los campos supera la longitud máxima permitida.", ex);

                    default:
                        throw new InvalidOperationException("Ocurrió un error en la base de datos al actualizar el jugador.", ex);
                }
            }
        }

        public async Task<Jugador> CrearJugadorAsync(Jugador jugador, CancellationToken ct)
        {
            try
            {
                _contexto.Jugadores.Add(jugador);
                await _contexto.SaveChangesAsync(ct);
                return jugador;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                switch (sqlEx.Number)
                {
                    case 2601:
                    case 2627:
                        throw new InvalidOperationException("El número de camiseta ya está asignado a otro jugador en este equipo.", ex);
                    case 547:
                        throw new InvalidOperationException("El equipo especificado no existe.", ex);
                    case 515:
                        throw new InvalidOperationException("Faltan campos obligatorios para guardar el jugador.", ex);
                    default:
                        throw;
                }
            }
        }

        public async Task<bool> EliminarJugadorAsync(int id, CancellationToken ct)
        {
            var jugador = await _contexto.Jugadores.FirstOrDefaultAsync(j => j.Id == id, ct);
            if (jugador == null)
            {
                return false;
            }

            try
            {
                _contexto.Jugadores.Remove(jugador);
                await _contexto.SaveChangesAsync(ct);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("El registro ya no existe o fue modificado concurrentemente.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    throw new InvalidOperationException("No se puede eliminar el jugador porque tiene registros asociados.", ex);
                }
                throw;
            }
        }

        public async Task<List<Jugador>> ListarJugadoresAsync(CancellationToken ct)
        {
            return await _contexto.Jugadores
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<Jugador?> ObtenerJugadorPorIdAsync(int id, CancellationToken ct)
        {
            return await _contexto.Jugadores
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.Id == id, ct);
        }
    }
}

