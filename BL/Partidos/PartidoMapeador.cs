using AB.Models.Partidos;

namespace BL.Partidos
{
    internal static class PartidoMapeador
    {
        public static PartidoDto ADto(Partido p) => new PartidoDto
        {
            Id = p.Id,
            EquipoLocalId = p.EquipoLocalId,
            EquipoVisitanteId = p.EquipoVisitanteId,
            Fecha = p.Fecha,
            Cancha = p.Cancha,
            GolesLocal = p.GolesLocal,
            GolesVisitante = p.GolesVisitante,
            Estado = p.Estado
        };
    }
}
