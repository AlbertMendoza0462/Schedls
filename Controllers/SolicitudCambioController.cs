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
    public class SolicitudCambioController : ControllerBase
    {
        public SolicitudCambioBLL _bll { get; set; }
        public SolicitudCambioController(SolicitudCambioBLL solicitudCambioBLL)
        {
            _bll = solicitudCambioBLL;
        }

        // GET: api/<SolicitudCambioController>
        [HttpGet("Aprobadas")]
        public async Task<IActionResult> GetListAprobadas()
        {
            try
            {
                return Ok(await _bll.ListarAprobadas());
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

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

        // GET api/<SolicitudCambioController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int solicitudCambioId)
        {
            try
            {
                if (solicitudCambioId < 1)
                {
                    throw new Exception($"El campo '{nameof(solicitudCambioId)}' debe ser mayor que 0.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var solicitudCambio = await _bll.Buscar(solicitudCambioId);
                return (solicitudCambio == null) ? NotFound() : Ok(solicitudCambio);
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // POST api/<SolicitudCambioController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SolicitudCambio solicitudCambio)
        {
            try
            {
                if (solicitudCambio.SolicitudCambioId < 0)
                {
                    throw new Exception($"El campo '{nameof(solicitudCambio.SolicitudCambioId)}' no debe ser negativo.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                solicitudCambio.FechaTurnoActual = new DateTime(
                    solicitudCambio.FechaTurnoActual.Year,
                    solicitudCambio.FechaTurnoActual.Month,
                    solicitudCambio.FechaTurnoActual.Day,
                    solicitudCambio.TurnoActual.FechaInicio.Hour,
                    solicitudCambio.TurnoActual.FechaInicio.Minute,
                    solicitudCambio.TurnoActual.FechaInicio.Second
                    );
                solicitudCambio.TurnoActual = null;

                solicitudCambio.FechaTurnoSolicitado = new DateTime(
                    solicitudCambio.FechaTurnoSolicitado.Year,
                    solicitudCambio.FechaTurnoSolicitado.Month,
                    solicitudCambio.FechaTurnoSolicitado.Day,
                    solicitudCambio.TurnoSolicitado.FechaInicio.Hour,
                    solicitudCambio.TurnoSolicitado.FechaInicio.Minute,
                    solicitudCambio.TurnoSolicitado.FechaInicio.Second
                    );
                solicitudCambio.TurnoSolicitado = null;

                var guardo = await _bll.Guardar(solicitudCambio);
                return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        [HttpPost("CambiarEstado")]
        public async Task<IActionResult> CambiarEstado([FromBody] SolicitudCambio solicitudCambio)
        {
            try
            {
                if (solicitudCambio.SolicitudCambioId < 0)
                {
                    throw new Exception($"El campo '{nameof(solicitudCambio.SolicitudCambioId)}' no debe ser negativo.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var guardo = await _bll.CambiarEstado(solicitudCambio);
                return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // DELETE api/<SolicitudCambioController>
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
                var solicitudCambio = await _bll.Buscar(id ?? 0);
                if (solicitudCambio != null)
                {
                    var guardo = await _bll.Eliminar(solicitudCambio);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo eliminar.");
                }
                else
                {
                    throw new Exception($"El SolicitudCambioId '{nameof(id)}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }
    }
}
