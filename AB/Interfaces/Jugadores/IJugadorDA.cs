using AB.Models.Jugadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace AB.Interfaces.Jugadores
{
    public interface IJugadorDA
    {
        Task<List<Jugador>> ListarJugadoresAsync(CancellationToken ct);

        Task<Jugador?> ObtenerJugadorPorIdAsync(int id, CancellationToken ct);

        Task<Jugador> CrearJugadorAsync(Jugador jugador, CancellationToken ct);

        Task<bool> ActualizarJugadorAsync(Jugador jugador, CancellationToken ct);

        Task<bool> EliminarJugadorAsync(int id, CancellationToken ct);
    }
}

