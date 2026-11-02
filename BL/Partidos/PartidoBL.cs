using AB.Interfaces.Partidos;
using AB.Models.Partidos;

namespace BL.Partidos
{
    public class PartidoBL : IPartidoBL
    {
        private readonly IPartidoDA _partidoDA;

        public PartidoBL(IPartidoDA partidoDA) => _partidoDA = partidoDA;

        public async Task<List<PartidoDto>> ListarPartidosAsync(CancellationToken ct) =>
            (await _partidoDA.ListarPartidosAsync(ct)).Select(PartidoMapeador.ADto).ToList();

        public async Task<PartidoDto?> ObtenerPartidoPorIdAsync(int id, CancellationToken ct)
        {
            var p = await _partidoDA.ObtenerPartidoPorIdAsync(id, ct);
            return p != null ? PartidoMapeador.ADto(p) : null;
        }

        public async Task<PartidoDto> CrearPartidoAsync(PartidoCrearDto dto, CancellationToken ct)
        {
            // Regla 1: Local y Visitante que no sean el mismo equipo
            if (dto.EquipoLocalId == dto.EquipoVisitanteId)
                throw new InvalidOperationException("El equipo local y visitante no pueden ser el mismo.");

            // Regla 2: sin choque de fecha (el DA ignora Cancelado y respeta excluirPartidoId)
            if (await _partidoDA.EquipoTienePartidoEnFechaAsync(dto.EquipoLocalId, dto.Fecha, null, ct))
                throw new InvalidOperationException("El equipo local ya tiene un partido programado a esa fecha y hora.");

            if (await _partidoDA.EquipoTienePartidoEnFechaAsync(dto.EquipoVisitanteId, dto.Fecha, null, ct))
                throw new InvalidOperationException("El equipo visitante ya tiene un partido programado a esa fecha y hora.");

            var partido = new Partido
            {
                EquipoLocalId = dto.EquipoLocalId,
                EquipoVisitanteId = dto.EquipoVisitanteId,
                Fecha = dto.Fecha,
                Cancha = dto.Cancha,
                GolesLocal = null,        
                GolesVisitante = null,
                Estado = EstadoPartido.Programado
            };

            await _partidoDA.CrearPartidoAsync(partido, ct);
            return PartidoMapeador.ADto(partido);
        }

        public async Task ActualizarAsync(int id, PartidoActualizarDto dto, CancellationToken ct)
        {
            var partido = await _partidoDA.ObtenerPartidoPorIdAsync(id, ct)
                ?? throw new KeyNotFoundException("Partido no encontrado.");

            // Regla 1
            if (partido.EquipoLocalId == partido.EquipoVisitanteId)
                throw new InvalidOperationException("El equipo local y visitante no pueden ser el mismo.");

            // Regla 2: solo validar si cambió la fecha; excluir el partido actual
            if (partido.Fecha != dto.Fecha)
            {
                if (await _partidoDA.EquipoTienePartidoEnFechaAsync(partido.EquipoLocalId, dto.Fecha, id, ct))
                    throw new InvalidOperationException("El equipo local ya tiene otro partido a esa fecha y hora.");
                if (await _partidoDA.EquipoTienePartidoEnFechaAsync(partido.EquipoVisitanteId, dto.Fecha, id, ct))
                    throw new InvalidOperationException("El equipo visitante ya tiene otro partido a esa fecha y hora.");
            }

            // Regla 3: goles solo si han Jugado
            if (dto.Estado != EstadoPartido.Jugado && (dto.GolesLocal.HasValue || dto.GolesVisitante.HasValue))
                throw new InvalidOperationException("Solo se pueden registrar goles cuando el partido está en estado 'Jugado'.");

            if (dto.Estado == EstadoPartido.Jugado && (!dto.GolesLocal.HasValue || !dto.GolesVisitante.HasValue))
                throw new InvalidOperationException("Debe registrar los goles de ambos equipos para marcar el partido como 'Jugado'.");

            partido.Fecha = dto.Fecha;
            partido.Cancha = dto.Cancha;
            partido.Estado = dto.Estado;
            partido.GolesLocal = dto.GolesLocal;
            partido.GolesVisitante = dto.GolesVisitante;

            await _partidoDA.ActualizarPartidoAsync(partido, ct);
        }

        public async Task EliminarAsync(int id, CancellationToken ct)
        {
            await _partidoDA.EliminarPartidoAsync(id, ct);
        }
    }
}