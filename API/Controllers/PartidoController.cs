using AB.Interfaces.Partidos;
using AB.Models.Partidos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidoController : ControllerBase
    {
        private readonly IPartidoBL _partidoBL;

        public PartidoController(IPartidoBL partidoBL) => _partidoBL = partidoBL;

        [HttpGet]
        public async Task<ActionResult<List<PartidoDto>>> Listar(CancellationToken ct) =>
            Ok(await _partidoBL.ListarPartidosAsync(ct));

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<PartidoDto>> ObtenerPorId(int id, CancellationToken ct)
        {
            var p = await _partidoBL.ObtenerPartidoPorIdAsync(id, ct);
            return p == null ? NotFound() : Ok(p);
        }

        [HttpPost]
        public async Task<ActionResult<PartidoDto>> Crear([FromBody] PartidoCrearDto dto, CancellationToken ct)
        {
            try
            {
                var nuevo = await _partidoBL.CrearPartidoAsync(dto, ct);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.Id }, nuevo);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ProblemDetails { Title = "Conflicto", Detail = ex.Message, Status = 409 });
            }
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] PartidoActualizarDto dto, CancellationToken ct)
        {
            try
            {
                await _partidoBL.ActualizarAsync(id, dto, ct);
                return NoContent(); 
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ProblemDetails { Title = "Conflicto", Detail = ex.Message, Status = 409 });
            }
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Eliminar(int id, CancellationToken ct)
        {
            try
            {
                await _partidoBL.EliminarAsync(id, ct);
                return NoContent(); 
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}