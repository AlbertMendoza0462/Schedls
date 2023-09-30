using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Schedls.BLL;
using Schedls.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Schedls.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ValidaToken")]
    public class EstadoSolicitudController : ControllerBase
    {
        public EstadoSolicitudBLL _bll { get; set; }
        public EstadoSolicitudController(EstadoSolicitudBLL estadoSolicitudBLL)
        {
            _bll = estadoSolicitudBLL;
        }

        // GET: api/<EstadoSolicitudController>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                return Ok(await _bll.Listar());
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // GET api/<EstadoSolicitudController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int estadoSolicitudId)
        {
            try
            {
                if (estadoSolicitudId < 1)
                {
                    throw new Exception($"El campo '{nameof(estadoSolicitudId)}' debe ser mayor que 0.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var estadoSolicitud = await _bll.Buscar(estadoSolicitudId);
                return (estadoSolicitud == null) ? NotFound() : Ok(estadoSolicitud);
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // POST api/<EstadoSolicitudController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EstadoSolicitud estadoSolicitud)
        {
            try
            {
                if (estadoSolicitud.EstadoSolicitudId < 0)
                {
                    throw new Exception($"El campo '{nameof(estadoSolicitud.EstadoSolicitudId)}' no debe ser negativo.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var guardo = await _bll.Guardar(estadoSolicitud);
                return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // DELETE api/<EstadoSolicitudController>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null || id < 0)
                {
                    throw new Exception($"El campo '{nameof(id)}' debe ser mayor que 0.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var estadoSolicitud = await _bll.Buscar(id ?? 0);
                if (estadoSolicitud != null)
                {
                    var guardo = await _bll.Eliminar(estadoSolicitud);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo eliminar.");
                }
                else
                {
                    throw new Exception($"El EstadoSolicitudId '{nameof(id)}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }
    }
}
