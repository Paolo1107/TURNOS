using Microsoft.AspNetCore.Mvc;
using ServicioBack.Entities.Models;
using ServicioBack.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly IServicioService _service;
        public ServicioController(IServicioService servicioService)
        {
            _service = servicioService;
        }
        // GET: api/<ServicioController>
        [HttpGet]
        public async Task<IActionResult> GetServicios()
        {
            try
            {
                var servicio = await _service.GetAll();
                if (servicio == null || !servicio.Any())
                {
                    return NotFound("No se encontraron servicios");
                }
                return Ok(servicio);

            }
            catch (Exception)
            {

                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        //GET con filtros
        [HttpGet("Filtrar")]
        public async Task<IActionResult> GetServicioFiltros([FromQuery] string promo, [FromQuery] string nombre)
        {
            try
            {
                var serviciosFiltrados = await _service.ConsultsFiltersAsync(promo, nombre);
                if (serviciosFiltrados == null || !serviciosFiltrados.Any())
                {
                    return NotFound("No se encontraron servicios que coincidan con lo filtrado");
                }
                return Ok(serviciosFiltrados);
            }
            catch (Exception)
            {

                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }

        }

        // POST api/<ServicioController>
        [HttpPost]
        public async Task<IActionResult> PostServicio([FromBody] TServicio servicio)
        {
            // Aquí puedes validar el objeto 'servicio' si es necesario
            if (servicio == null)
            {

                return BadRequest("El servicio no puede ser nulo.");
            }

            // Asegúrate de que el Id esté en 0 para un nuevo servicio
            if (servicio.Id != 0)
            {
                return BadRequest("El Id debe ser 0 para crear un nuevo servicio.");
            }
            var result = await _service.SaveAsync(servicio);
            if (result)
            {
                return Ok("Servicio registrado correctamente");
            }
            else
            {
                return BadRequest("Error al agregar el servicio."); // Devuelve un 400 Bad Request si hubo un problema.
            }
        }

        // PUT api/<ServicioController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicio(int id, [FromBody] TServicio servicio)
        {
            if (id != servicio.Id)
            {
                return BadRequest("El id del servicio no coincide");
            }
            var result = await _service.SaveAsync(servicio);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound("Servicio no encontrado.");
            }
        }

        // DELETE api/<ServicioController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            var result = await _service.DeleteLogicAsync(id);
            if (!result)
            {
                return NotFound("Servicio no encontrado.");
            }
            return Ok("Servicio dado de baja correctamente.");
        }
    }
}
