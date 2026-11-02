using AB.Interfaces.Partidos;
using AB.Models.Partidos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Partidos
{
    public class PartidoDA : IPartidoDA
    {
        private readonly LigaDeportivaContexto _contexto;

        public PartidoDA(LigaDeportivaContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ActualizarPartidoAsync(Partido partido, CancellationToken ct)
        {
            var partidoExistente = await _contexto.Partidos.FirstOrDefaultAsync(p => p.Id == partido.Id, ct);

            if (partidoExistente == null)
            {
                return false;
            }

            try
            {
                partidoExistente.EquipoLocalId = partido.EquipoLocalId;
                partidoExistente.EquipoVisitanteId = partido.EquipoVisitanteId;
                partidoExistente.Fecha = partido.Fecha;
                partidoExistente.Cancha = partido.Cancha;
                partidoExistente.Estado = partido.Estado;
                partidoExistente.GolesLocal = partido.GolesLocal;
                partidoExistente.GolesVisitante = partido.GolesVisitante;

                await _contexto.SaveChangesAsync(ct);
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidOperationException("El partido fue modificado o eliminado por otro proceso.", ex);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                switch (sqlEx.Number)
                {
                    case 547:
                        throw new InvalidOperationException("El equipo local o visitante especificado no existe.", ex);

                    case 515:
                        throw new InvalidOperationException("Uno o más campos obligatorios no pueden quedar vacíos.", ex);

                    case 2628:
                    case 8152:
                        throw new InvalidOperationException("Uno de los campos supera la longitud máxima permitida.", ex);

                    default:
                        throw new InvalidOperationException("Ocurrió un error en la base de datos al actualizar el partido.", ex);
                }
            }
        }

        public async Task<Partido> CrearPartidoAsync(Partido partido, CancellationToken ct)
        {
            try
            {
                _contexto.Partidos.Add(partido);
                await _contexto.SaveChangesAsync(ct);
                return partido;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                switch (sqlEx.Number)
                {
                    case 547:
                        throw new InvalidOperationException("El equipo local o visitante especificado no existe.", ex);
                    case 515:
                        throw new InvalidOperationException("Faltan campos obligatorios para guardar el partido.", ex);
                    case 2628:
                    case 8152:
                        throw new InvalidOperationException("Uno de los campos supera la longitud máxima permitida.", ex);
                    default:
                        throw;
                }
            }
        }

        public async Task<bool> EliminarPartidoAsync(int id, CancellationToken ct)
        {
            var partido = await _contexto.Partidos.FirstOrDefaultAsync(p => p.Id == id, ct);
            if (partido == null)
            {
                return false;
            }

            try
            {
                _contexto.Partidos.Remove(partido);
                await _contexto.SaveChangesAsync(ct);
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidOperationException("El registro ya no existe o fue modificado concurrentemente.", ex);
            }
        }

        public async Task<bool> EquipoTienePartidoEnFechaAsync(int equipoId, DateTime fecha, int? excluirPartidoId, CancellationToken ct)
        {
            return await _contexto.Partidos.AnyAsync(p =>
                (p.EquipoLocalId == equipoId || p.EquipoVisitanteId == equipoId)
                && p.Fecha == fecha
                && p.Estado != EstadoPartido.Cancelado
                && (excluirPartidoId == null || p.Id != excluirPartidoId), ct);
        }

        public async Task<List<Partido>> ListarPartidosAsync(CancellationToken ct)
        {
            return await _contexto.Partidos
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<Partido?> ObtenerPartidoPorIdAsync(int id, CancellationToken ct)
        {
            return await _contexto.Partidos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, ct);
        }
    }
}
