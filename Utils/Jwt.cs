using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Schedls.BLL;
using System.Security.Claims;

namespace Schedls.Utils
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public async static Task<bool> ValidarToken(ClaimsIdentity identity, UsuarioBLL usuarioBLL)
        {
            if (identity.Claims.Count() == 0)
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == "EmpleadoId");
                if (claim != null)
                {
                    int empleadoId = Int32.Parse(claim.Value);
                    var empleado = await usuarioBLL.BuscarEmpleado(empleadoId);
                    return (empleado != null);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
