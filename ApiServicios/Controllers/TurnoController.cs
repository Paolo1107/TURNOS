using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using ServicioBack.Entities.Models;
using ServicioBack.Repositories.Turnos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoRepository _turnoRepository;
        public TurnoController(ITurnoRepository turnoRepository)
        {
            _turnoRepository = turnoRepository;
        }

        // GET: api/<TurnoController>
        [HttpGet]
        public async Task<IActionResult> GetTurnos()
        {
            try
            {
                var turno = await _turnoRepository.GetAllTurnos();
                if (turno == null || !turno.Any())
                {
                    return NotFound("No se encontraron turnos");
                }
                return Ok(turno);
            }
            catch (Exception)
            {

                return StatusCode(500, "Error interno");
            }
        }

        [HttpGet("Filtros")]
        public async Task<IActionResult> GetTurnosFiltros([FromQuery] string cliente, [FromQuery] DateTime fecha, [FromQuery] string hora)
        {
            try
            {
                var ff = await _turnoRepository.GetTurnosFilters(cliente, fecha, hora);
                if (ff == null || !ff.Any())
                {
                    return NotFound("No hay turnos con los datos solicitados");
                }
                return Ok(ff);
            }
            catch (Exception)
            {

                return BadRequest("Error...No se pudieron filtrar los datos");
            }
        }

        // POST api/<TurnoController>
        [HttpPost("New Turno")]
        public async Task<IActionResult> PostTurnos([FromBody] TTurno tt)
        {
            if (tt == null)
            {
                return BadRequest("El turno no puede ser nulo");
            }
            var ts = await _turnoRepository.AddTurnos(tt);
            if (ts)
            {
                return Ok("Turno agregado correctamente");
            }
            else
            {
                return BadRequest("no se pudo agregar un turno...Error, no puede superar los 45 días");
            }
        }

        // PUT api/<TurnoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurnos([FromBody] TTurno turno, int id)
        {

            var tp = await _turnoRepository.UpdateTurnos(turno, id);
            if (tp)
            {
                return Ok("Turno actualizado correctamente");
            }
            else
            {
                return NotFound("Error al actualizar el turno");
            }
        }

        // DELETE api/<TurnoController>/5
        [HttpDelete]
        public async Task<IActionResult> DeleteTurno([FromQuery] int idTur, [FromQuery] int idSer, [FromQuery] string motivo)
        {
            try
            {
                var cancelado = await _turnoRepository.LowTurnos(idTur, idSer, motivo);

                if (cancelado)
                {
                    return Ok("Cancelación exitosa.");
                }
                else
                {
                    return BadRequest("Error al cancelar el turno. No se encontró el turno o servicio.");
                }
            }
            catch (Exception ex)
            {
                // Puedes agregar más detalles si lo prefieres, como el mensaje de excepción
                return BadRequest($"Error al procesar la solicitud: {ex.Message}");
            }
        }

    }
}
