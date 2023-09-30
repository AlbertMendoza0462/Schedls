using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Schedls.BLL;
using Schedls.DAL;
using Schedls.Models;
using System.Security.Claims;
using System.Text.Json;

namespace Schedls.Politicas
{
    public class ValidaTokenRequirement : IAuthorizationRequirement
    {

    }

    public class ValidaTokenHandler : AuthorizationHandler<ValidaTokenRequirement>
    {
        public UsuarioBLL _bll { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ValidaTokenHandler(UsuarioBLL bll, IHttpContextAccessor httpContextAccessor)
        {
            _bll = bll;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<Task> HandleRequirementAsync(
            AuthorizationHandlerContext context, ValidaTokenRequirement requirement)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            var mensaje = "";

            if (identity is null)
            {
                mensaje = "identity is null";
            }
            else
            {
                mensaje = "identity is not null";
                if (identity.Claims.Count() != 0)
                {
                    mensaje = "identity.Claims.Count() != 0";
                    var claim = identity.Claims.FirstOrDefault(x => x.Type == "Empleado");
                    if (claim != null)
                    {
                        var jti = identity.Claims.FirstOrDefault(x => x.Type == "jti")?.Value;
                        mensaje = "claim != null";
                        var empleadoClaim = JsonSerializer.Deserialize<Empleado>(claim.Value);
                        if (empleadoClaim != null)
                        {
                            mensaje = "empleadoClaim != null";
                            mensaje = (_bll is null) ? "Es nulo" : "No es nulo";
                            try
                            {
                                var jtiObtenido = await _bll.ObtenerSesion(empleadoClaim);
                                if (!jtiObtenido.IsNullOrEmpty() && jti == jtiObtenido)
                                {
                                    mensaje = "Existe";
                                    context.Succeed(requirement);
                                    return Task.CompletedTask;
                                }
                                else
                                {
                                    mensaje = "Sesion Invalida";
                                }
                            }
                            catch (Exception e)
                            {
                                mensaje = e.Source + ": " + e.Message;
                            }
                        }
                        else
                        {
                            mensaje = "empleadoClaim == null";
                        }
                    }
                    else
                    {
                        mensaje = "claim == null";
                    }
                    if (_httpContextAccessor.HttpContext != null)
                    {
                        _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        _httpContextAccessor.HttpContext.Response.ContentType = "application/json";
                        await _httpContextAccessor.HttpContext.Response.WriteAsync("{\"error\": \"" + mensaje + "\"}");
                        await _httpContextAccessor.HttpContext.Response.CompleteAsync(); //Needed this or my integration tests failed with a System.IO exception.
                    }
                    context.Fail();
                    return Task.FromException(new Exception(mensaje));
                }
                else
                {
                    mensaje = "identity.Claims.Count() == 0";
                }
            }
            return Task.CompletedTask;
        }
    }
}
