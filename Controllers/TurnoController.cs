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
    public class TurnoController : ControllerBase
    {
        public TurnoBLL _bll { get; set; }
        public SolicitudCambioBLL _solicitudCambioBll { get; set; }
        public TurnoController(TurnoBLL turnoBLL, SolicitudCambioBLL solicitudCambioBll)
        {
            _bll = turnoBLL;
            _solicitudCambioBll = solicitudCambioBll;
        }

        // GET: api/<TurnoController>
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

        [HttpGet("eventos")]
        public async Task<IActionResult> GetListEventos()
        {
            try
            {
                var aprobadas = await _solicitudCambioBll.ListarAprobadas();
                return Ok(await _bll.ListarEventos(aprobadas));
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.ToString());
            }
        }

        // GET api/<TurnoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int turnoId)
        {
            try
            {
                if (turnoId < 1)
                {
                    throw new Exception($"El campo '{nameof(turnoId)}' debe ser mayor que 0.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var turno = await _bll.Buscar(turnoId);
                return (turno == null) ? NotFound() : Ok(turno);
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // POST api/<TurnoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Turno turno)
        {
            try
            {
                if (turno.TurnoId < 0)
                {
                    throw new Exception($"El campo '{nameof(turno.TurnoId)}' no debe ser negativo.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var guardo = await _bll.Guardar(turno);
                return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // DELETE api/<TurnoController>
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
                var turno = await _bll.Buscar(id ?? 0);
                if (turno != null)
                {
                    var guardo = await _bll.Eliminar(turno);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo eliminar.");
                }
                else
                {
                    throw new Exception($"El TurnoId '{nameof(id)}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }
    }
}
