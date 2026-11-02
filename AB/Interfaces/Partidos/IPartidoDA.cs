using AB.Models.Partidos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AB.Interfaces.Partidos
{
    public interface IPartidoDA
    {
        Task<List<Partido>> ListarPartidosAsync(CancellationToken ct);
        Task<Partido?> ObtenerPartidoPorIdAsync(int id, CancellationToken ct);
        Task<Partido> CrearPartidoAsync(Partido partido, CancellationToken ct);
        Task<bool> ActualizarPartidoAsync(Partido partido, CancellationToken ct);
        Task<bool> EliminarPartidoAsync(int id, CancellationToken ct);
        Task<bool> EquipoTienePartidoEnFechaAsync(int equipoId, DateTime fecha, int? excluirPartidoId, CancellationToken ct);
    }
}
