using AB.Models.Equipos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AB.Interfaces.Equipos
{
    public interface IEquipoDA
    {
        Task<List<Equipo>> ListarEquiposAsync(CancellationToken ct);
        
        Task<Equipo?> ObtenerEquipoPorIdAsync(int id, CancellationToken ct);

        Task<Equipo> CrearEquipoAsync(Equipo equipo, CancellationToken ct);

        Task<bool> ActualizarEquipoAsync(Equipo equipo, CancellationToken ct);

        Task<bool> EliminarEquipoAsync(int id, CancellationToken ct);

    }
}
