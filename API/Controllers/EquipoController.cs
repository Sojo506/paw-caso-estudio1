using AB.Interfaces.Equipos;
using AB.Models.Equipos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly IEquipoBL _bl;

        public EquipoController(IEquipoBL bl) => _bl = bl;


        //GET: api/<EquipoController>
        [HttpGet]
        [ProducesResponseType<IReadOnlyList<EquipoDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyList<EquipoDto>>> Listar(CancellationToken ct)
        {
            var Equipos = await _bl.ListarEquiposAsync(ct);
            return Ok(Equipos);
        }

        //GET: api/<EquipoController>/id
        [HttpGet("{id:int:min(1)}")]
        [ProducesResponseType<IReadOnlyList<EquipoDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EquipoDto>> ObtenerEquipoPorId(int id, CancellationToken ct)
        {
            var equipo = await _bl.ObtenerEquipoPorIdAsync(id, ct);
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        // POST api/<AgenteController>
        [HttpPost]
        public async Task<ActionResult<EquipoDto>> CrearEquipo([FromBody] EquipoCrearDto equipo, CancellationToken ct)
        {
            var nuevoEquipo = await _bl.CrearEquipoAsync(equipo, ct);
            return CreatedAtAction(nameof(ObtenerEquipoPorId), new { id = nuevoEquipo.Id }, nuevoEquipo);
        }

        // PUT api/<AgenteController>/id
        [HttpPut("{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Actualizar(int id, [FromBody] EquipoActualizarDto equipo, CancellationToken ct)
        {
             await _bl.ActualizarAsync(id, equipo, ct);
            return NoContent();
        }

        // DELETE api/<AgenteController>/5
        [HttpDelete("{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Eliminar(int id, CancellationToken ct)
        {
            await _bl.EliminarAsync(id, ct);
            return NoContent();
        }






    }
}
