using AB.Interfaces.Jugadores;
using AB.Models.Jugadores;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadorController : ControllerBase
    {
        private readonly IJugadorBL _bl;

        public JugadorController(IJugadorBL bl)
        {
            _bl = bl;
        }

        // GET: api/<JugadorController>
        [HttpGet]
        [ProducesResponseType<IReadOnlyList<JugadorDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyList<JugadorDto>>> Listar(CancellationToken ct)
        {
            var jugadores = await _bl.ListarJugadoresAsync(ct);
            return Ok(jugadores);
        }

        // GET: api/<JugadorController>/id
        [HttpGet("{id:int:min(1)}")]
        [ProducesResponseType<JugadorDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JugadorDto>> ObtenerJugadorPorId(int id, CancellationToken ct)
        {
            var jugador = await _bl.ObtenerJugadorPorIdAsync(id, ct);
            if (jugador == null)
            {
                return NotFound();
            }
            return Ok(jugador);
        }

        // POST api/<JugadorController>
        [HttpPost]
        [ProducesResponseType<JugadorDto>(StatusCodes.Status201Created)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<JugadorDto>> CrearJugador([FromBody] JugadorCrearDto jugador, CancellationToken ct)
        {
            try
            {
                var nuevoJugador = await _bl.CrearJugadorAsync(jugador, ct);
                return CreatedAtAction(nameof(ObtenerJugadorPorId), new { id = nuevoJugador.Id }, nuevoJugador);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ProblemDetails { Detail = ex.Message, Status = StatusCodes.Status409Conflict });
            }
        }

        // PUT api/<JugadorController>/id
        [HttpPut("{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Actualizar(int id, [FromBody] JugadorActualizarDto jugador, CancellationToken ct)
        {
            try
            {
                await _bl.ActualizarAsync(id, jugador, ct);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message, Status = StatusCodes.Status404NotFound });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ProblemDetails { Detail = ex.Message, Status = StatusCodes.Status409Conflict });
            }
        }

        // DELETE api/<JugadorController>/id
        [HttpDelete("{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Eliminar(int id, CancellationToken ct)
        {
            try
            {
                await _bl.EliminarAsync(id, ct);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message, Status = StatusCodes.Status404NotFound });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ProblemDetails { Detail = ex.Message, Status = StatusCodes.Status409Conflict });
            }
        }
    }
}

