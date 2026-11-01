using AB.Models.Equipos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AB.Interfaces.Equipos
{
    public interface IEquipoBL
    {
        Task<List<EquipoDto>> ListarEquiposAsync(CancellationToken ct);

        Task<EquipoDto?> ObtenerEquipoPorIdAsync(int id, CancellationToken ct);

        Task<EquipoDto> CrearEquipoAsync(EquipoCrearDto equipo, CancellationToken ct);

        Task ActualizarAsync(int id, EquipoActualizarDto dto, CancellationToken ct);

        Task EliminarAsync(int id, CancellationToken ct);
    }
}
