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
    public class TipoTurnoController : ControllerBase
    {
        public TipoTurnoBLL _bll { get; set; }
        public TipoTurnoController(TipoTurnoBLL tipoTurnoBLL)
        {
            _bll = tipoTurnoBLL;
        }

        // GET: api/<TipoTurnoController>
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

        // GET api/<TipoTurnoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int tipoTurnoId)
        {
            try
            {
                if (tipoTurnoId < 1)
                {
                    throw new Exception($"El campo '{nameof(tipoTurnoId)}' debe ser mayor que 0.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var tipoTurno = await _bll.Buscar(tipoTurnoId);
                return (tipoTurno == null) ? NotFound() : Ok(tipoTurno);
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // POST api/<TipoTurnoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TipoTurno tipoTurno)
        {
            try
            {
                if (tipoTurno.TipoTurnoId < 0)
                {
                    throw new Exception($"El campo '{nameof(tipoTurno.TipoTurnoId)}' no debe ser negativo.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var guardo = await _bll.Guardar(tipoTurno);
                return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // DELETE api/<TipoTurnoController>
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
                var tipoTurno = await _bll.Buscar(id ?? 0);
                if (tipoTurno != null)
                {
                    var guardo = await _bll.Eliminar(tipoTurno);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo eliminar.");
                }
                else
                {
                    throw new Exception($"El TipoTurnoId '{nameof(id)}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }
    }
}
