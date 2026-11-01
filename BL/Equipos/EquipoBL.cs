using AB.Interfaces.Equipos;
using AB.Models.Equipos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Equipos
{
    public class EquipoBL : IEquipoBL
    {
        private readonly IEquipoDA _da;

        public EquipoBL(IEquipoDA da)
        {
            _da = da;
        }

        public async Task ActualizarAsync(int id, EquipoActualizarDto dto,CancellationToken ct)
        {
            var actualizado = await _da.ActualizarEquipoAsync(new Equipo
            {
                Id = id,
                Nombre = dto.Nombre,
                Ciudad = dto.Ciudad,
                Estado = dto.Estado,
            },ct);

            if(!actualizado)
            {
                throw new InvalidOperationException("No se pudo actualizar el equipo.");
            }
        }

        public async Task<EquipoDto> CrearEquipoAsync(EquipoCrearDto dto, CancellationToken ct)
        {
            var Equipo = new Equipo
            {
                Nombre = dto.Nombre,
                Ciudad = dto.Ciudad,
                Estado = dto.Estado,
                FechaFundacion = DateTime.UtcNow,
            };
                await _da.CrearEquipoAsync(Equipo, ct);
            return EquipoMapeador.ADto(Equipo);

        }

        public async Task EliminarAsync(int id, CancellationToken ct)
        {
            var eliminado = await _da.EliminarEquipoAsync(id, ct);

            if(!eliminado)
            {
                throw new InvalidOperationException("No se pudo eliminar el equipo.");
            }
        }

        public async Task<List<EquipoDto>> ListarEquiposAsync(CancellationToken ct)
        {
            var equipos = await _da.ListarEquiposAsync(ct);
            return equipos.Select(EquipoMapeador.ADto).ToList();
        }

        public async Task<EquipoDto?> ObtenerEquipoPorIdAsync(int id, CancellationToken ct)
        {
            var equipo = await _da.ObtenerEquipoPorIdAsync(id, ct);
            return equipo != null ? EquipoMapeador.ADto(equipo) : null;
        }
    }
}

