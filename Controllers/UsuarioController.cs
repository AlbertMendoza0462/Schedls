using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using Schedls.BLL;
using Schedls.Models;
using Schedls.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Schedls.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ValidaToken")]
    public class UsuarioController : ControllerBase
    {
        public UsuarioBLL _bll { get; set; }
        public IConfiguration _config { get; set; }

        public UsuarioController(UsuarioBLL usuarioBLL, IConfiguration config)
        {
            _bll = usuarioBLL;
            _config = config;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            try
            {
                var empleado = await _bll.BuscarPorCorreoYClave(login.Correo, login.Clave);
                if (empleado != null)
                {
                    var jwt = _config.GetSection("Jwt").Get<Jwt>();
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Empleado", JsonSerializer.Serialize(empleado) ?? "")
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                    var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var tokenData = new JwtSecurityToken
                    (
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddHours(12),
                        signingCredentials: singIn
                    );

                    var token = new JwtSecurityTokenHandler().WriteToken(tokenData);

                    var jti = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

                    var guardo = await _bll.GuardarSesion(empleado, jti ?? "");

                    return (guardo) ? Ok(token) : Problem("El token no se pudo guardar. No se puede entregar el token generado.");
                }
                else { return Unauthorized(); }
            }
            catch (Exception e)
            {

                return Problem(e.Source + ": " + e.Message);
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] Empleado empleado)
        {
            var contexto = HttpContext.User.Identity as ClaimsIdentity;
            if (contexto != null)
            {
                var claim = contexto.Claims.FirstOrDefault(x => x.Type == "Empleado");
                if (claim != null)
                {
                    empleado.EmpleadoId = JsonSerializer.Deserialize<Empleado>(claim.Value)?.EmpleadoId ?? 0;
                }
            }

            try
            {
                var existe = await _bll.Existe(empleado.EmpleadoId);
                if (existe)
                {
                    var guardo = await _bll.EliminarSesion(empleado);

                    return (guardo) ? Ok() : Problem("La sesion no se pudo eliminar.");
                }
                else { return Unauthorized(); }
            }
            catch (Exception e)
            {

                return Problem(e.Source + ": " + e.Message);
            }
        }

        [HttpGet("ValidaSesion")]
        public IActionResult ValidaSesion()
        {
            return Ok();
        }

        // POST api/CambiarClave
        [HttpPost("CambiarClave")]
        public async Task<IActionResult> CambiarClave([FromBody] Claves claves)
        {
            try
            {
                if (claves.ClaveNueva != claves.ClaveConfirmacion)
                {
                    throw new Exception($"Las clave nueva y la clave de confirmación no coinciden.");
                }

                Usuario usuario = new Usuario();
                usuario.Clave = claves.ClaveNueva;

                var contexto = HttpContext.User.Identity as ClaimsIdentity;
                if (contexto != null)
                {
                    var claim = contexto.Claims.FirstOrDefault(x => x.Type == "Empleado");
                    if (claim != null)
                    {
                        usuario.UsuarioId = JsonSerializer.Deserialize<Empleado>(claim.Value)?.EmpleadoId ?? 0;
                    }
                }

                if (usuario.UsuarioId < 0)
                {
                    throw new Exception($"El campo '{nameof(usuario.UsuarioId)}' no debe ser negativo.");
                }

                var guardo = await _bll.CambiarClave(usuario, claves.ClaveActual);
                return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // GET: api/<UsuarioController>
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

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int usuarioId)
        {
            try
            {
                if (usuarioId < 1)
                {
                    throw new Exception($"El campo '{nameof(usuarioId)}' debe ser mayor que 0.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var usuario = await _bll.BuscarEmpleado(usuarioId);
                return (usuario == null) ? NotFound() : Ok(usuario);
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario.UsuarioId < 0)
                {
                    throw new Exception($"El campo '{nameof(usuario.UsuarioId)}' no debe ser negativo.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var guardo = await _bll.Guardar(usuario);
                return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // DELETE api/<UsuarioController>
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
                var empleado = await _bll.BuscarEmpleado(id ?? 0); ;
                if (empleado != null)
                {
                    var guardo = await _bll.Eliminar(empleado);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo eliminar.");
                }
                else
                {
                    throw new Exception($"El EmpleadoId '{nameof(id)}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }
    }
}
