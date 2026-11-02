using AB.Interfaces.Equipos;
using AB.Models.Equipos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Equipos
{
    public class EquipoDA : IEquipoDA
    {
        private readonly LigaDeportivaContexto _contexto;

        public EquipoDA(LigaDeportivaContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ActualizarEquipoAsync(Equipo equipo, CancellationToken ct)
        {
            var EquipoExistente = await _contexto.Equipos.FirstOrDefaultAsync(a => a.Id == equipo.Id, ct);

            if (EquipoExistente == null)
            {
                return false;
            }

            try
            {
                EquipoExistente.Nombre = equipo.Nombre;
                EquipoExistente.Ciudad = equipo.Ciudad;
                EquipoExistente.FechaFundacion = equipo.FechaFundacion;
                EquipoExistente.Estado = equipo.Estado;
                await _contexto.SaveChangesAsync(ct);
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // El registro fue alterado o borrado concurrentemente
                throw new InvalidOperationException("El equipo fue modificado o eliminado por otro proceso.", ex);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                switch (sqlEx.Number)
                {
                    case 2601:
                    case 2627:
                        throw new InvalidOperationException("Ya existe otro equipo con ese mismo nombre o identificador único.", ex);

                    case 547:
                        throw new InvalidOperationException("Uno de los datos relacionados no es válido o viola una restricción de integridad.", ex);

                    case 515:
                        throw new InvalidOperationException("Uno o más campos obligatorios no pueden quedar vacíos.", ex);

                    case 2628:
                    case 8152:
                        throw new InvalidOperationException("Uno de los campos supera la longitud máxima permitida.", ex);

                    default:
                        throw new InvalidOperationException("Ocurrió un error en la base de datos al actualizar el equipo.", ex);
                }
            }
        }

        public async Task<Equipo> CrearEquipoAsync(Equipo equipo, CancellationToken ct)
        {
            try 
            {
                _contexto.Equipos.Add(equipo);
                await _contexto.SaveChangesAsync(ct);
                return equipo;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                switch(sqlEx.Number)
                {
                    case 2601: // Violación de índice único

                    case 2627: // Violación de restricción única
                        throw new InvalidOperationException("El equipo ya existe.");
                    case 547 : // Violación de restricción de clave foránea
                        throw new InvalidOperationException("Uno de los elementos relacionados (como el torneo, liga o categoría) no existe.\", ex");
                    case 515: // Intento de insertar NULL en columna no permitida
                        throw new InvalidOperationException("Faltan campos obligatorios para guardar el equipo.", ex);
                    default:
                        // Relanza la excepción original si no es un error de negocio previsto
                        throw;
                }
            }
        }

        public async Task<bool> EliminarEquipoAsync(int id, CancellationToken ct)
        {
           var equipo = await _contexto.Equipos.FirstOrDefaultAsync(a => a.Id == id, ct);
            if (equipo == null)
            {
                return false;
            }
            
            try {
                _contexto.Equipos.Remove(equipo);
                await _contexto.SaveChangesAsync(ct);
                return true;
            }
            catch (DbUpdateConcurrencyException) 
            {
                // El registro ya fue eliminado o modificado en otra transacción
                throw new InvalidOperationException("El registro ya no existe o fue modificado concurrentemente.");
            }
            catch (DbUpdateException ex)
            {
                //vamos a verificar si la causa del problema es una FK
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    throw new InvalidOperationException("No se puede eliminar el equipo porque tiene jugadores asociados.");
                }
                throw;
            }   
        }

        public async Task<List<Equipo>> ListarEquiposAsync(CancellationToken ct)
        {
            return await _contexto.Equipos.
                AsNoTracking().
               //.include(a => a.Jugadores)
                ToListAsync(ct);

            //agregar .include(a => a.Jugadores) para traer los datos de los jugadores pertenecientes a cada equipo
        }

        public async Task<Equipo?> ObtenerEquipoPorIdAsync(int id, CancellationToken ct)
        {
           return await _contexto.Equipos.
                AsNoTracking().
                FirstOrDefaultAsync(a => a.Id == id, ct);
        }

        public Task<bool> TienePartidosPendientesAsync(int equipoId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
