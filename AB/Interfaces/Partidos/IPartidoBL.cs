using AB.Models.Partidos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AB.Interfaces.Partidos
{
    public interface IPartidoBL
    {
        Task<List<PartidoDto>> ListarPartidosAsync(CancellationToken ct);
        Task<PartidoDto?> ObtenerPartidoPorIdAsync(int id, CancellationToken ct);
        Task<PartidoDto> CrearPartidoAsync(PartidoCrearDto dto, CancellationToken ct);
        Task ActualizarAsync(int id, PartidoActualizarDto dto, CancellationToken ct);
        Task EliminarAsync(int id, CancellationToken ct);
    }
}
