using AB.Models.Jugadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace AB.Interfaces.Jugadores
{
    public interface IJugadorBL
    {
        Task<List<JugadorDto>> ListarJugadoresAsync(CancellationToken ct);

        Task<JugadorDto?> ObtenerJugadorPorIdAsync(int id, CancellationToken ct);

        Task<JugadorDto> CrearJugadorAsync(JugadorCrearDto jugador, CancellationToken ct);

        Task ActualizarAsync(int id, JugadorActualizarDto dto, CancellationToken ct);

        Task EliminarAsync(int id, CancellationToken ct);
    }
}

